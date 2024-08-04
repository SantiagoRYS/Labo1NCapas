using BLL.Exceptions;
using DAL;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Products
    {
        public async Task<Product> CreateAsync(Product product)
        {
            Product productResult = null;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                // Buscar si el nombre del producto existe
                Product productSearch = await repository.RetrieveAsync<Product>(c => c.ProductName == product.ProductName);
                if (productSearch == null)
                {
                    // No existe, podemos crearlo
                    productResult = await repository.CreateAsync(product);
                }
                else
                {
                    // Podríamos aqui lanzar una exepciòn
                    // para notificar que el producto ya existe.
                    // Podriamos incluso crear una capa de Excepciones
                    // personalizadas y consumirla desde otras  
                    // capas.
                    ProductExceptions.ThrowProductAlreadyExistException(productSearch.ProductName);
                }

            }
            return productResult!;
        }

        public async Task<Product> RetrieveByIDAsync(int id)
        {
            Product result = null;

            using (var repository = RepositoryFactory.CreateRepository())
            {
                Product product = await repository.RetrieveAsync<Product>(c => c.Id == id);

                // Check if product was found
                if (product == null)
                {
                    // Throw a ProductNotFoundException (assuming you have this class)
                    ProductExceptions.ThrowInvalidProductIdException(id);
                }

                return product!;
            }
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            bool Result = false;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                // Validar que el nombre del producto no exista
                Product productSearch = await repository.RetrieveAsync<Product>(
                    c => c.ProductName == product.ProductName && c.Id != product.Id);

                if (productSearch == null)
                {
                    // No existe
                    Result = await repository.UpdateAsync(product);
                }
                else
                {
                    // Podemos implementar alguna lógica para
                    // indicar que no se pudo modificar
                    ProductExceptions.ThrowProductAlreadyExistException(
                        productSearch.ProductName);
                }
            }
            return Result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool Result = false;
            // Buscar un producto para ver si tiene Orders (Ordenes de Compra)
            var product = await RetrieveByIDAsync(id);
            if (product != null)
            {
                // Eliminar el producto
                using (var repository = RepositoryFactory.CreateRepository())
                {
                    Result = await repository.DeleteAsync(product);
                }
            }
            else
            {
                // Podemos implementar alguna lógica
                // para indicar que el producto no existe
                ProductExceptions.ThrowInvalidProductIdException(id);
            }
            return Result;
        }

        public async Task<List<Product>> RetrieveAllAsync()
        {
            List<Product> Result = null;

            using (var r = RepositoryFactory.CreateRepository())
            {
                // Define el criterio de filtro para obtener todos los productos.
                Expression<Func<Product, bool>> allProductsCriteria = x => true;
                Result = await r.FilterAsync<Product>(allProductsCriteria);
            }
            return Result;
        }
    }
}
