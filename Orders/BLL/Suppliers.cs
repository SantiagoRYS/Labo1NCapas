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
    public class Suppliers
    {
        public async Task<Supplier> CreateAsync(Supplier supplier)
        {
            Supplier supplierResult = null;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                // Buscar si el nombre del proveedor existe
                Supplier supplierSearch = await repository.RetrieveAsync<Supplier>(c => c.CompanyName == supplier.CompanyName);
                if (supplierSearch == null)
                {
                    // No existe, podemos crearlo
                    supplierResult = await repository.CreateAsync(supplier);
                }
                else
                {
                    // Podríamos aqui lanzar una exepciòn
                    // para notificar que el producto ya existe.
                    // Podriamos incluso crear una capa de Excepciones
                    // personalizadas y consumirla desde otras  
                    // capas.
                    SupplierExceptions.ThrowSupplierAlreadyExistException(supplierSearch.CompanyName);
                }

            }
            return supplierResult!;
        }

        public async Task<Supplier> RetrieveByIDAsync(int id)
        {
            Supplier result = null;

            using (var repository = RepositoryFactory.CreateRepository())
            {
                Supplier supplier = await repository.RetrieveAsync<Supplier>(c => c.Id == id);

                // Check if supplier was found
                if (supplier == null)
                {
                    // Throw a ProductNotFoundException (assuming you have this class)
                    SupplierExceptions.ThrowInvalidSupplierIdException(id);
                }

                return supplier!;
            }
        }

        public async Task<bool> UpdateAsync(Supplier supplier)
        {
            bool Result = false;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                // Validar que el nombre del proveedor no exista
                Supplier supplierSearch = await repository.RetrieveAsync<Supplier>(
                    c => c.CompanyName == supplier.CompanyName && c.Id != supplier.Id);

                if (supplierSearch == null)
                {
                    // No existe
                    Result = await repository.UpdateAsync(supplier);
                }
                else
                {
                    // Podemos implementar alguna lógica para
                    // indicar que no se pudo modificar
                    SupplierExceptions.ThrowSupplierAlreadyExistException(
                        supplierSearch.CompanyName);
                }
            }
            return Result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool Result = false;
            // Buscar un proveedor para ver si tiene Orders (Ordenes de Compra)
            var supplier = await RetrieveByIDAsync(id);
            if (supplier != null)
            {
                // Eliminar el proveedor
                using (var repository = RepositoryFactory.CreateRepository())
                {
                    Result = await repository.DeleteAsync(supplier);
                }
            }
            else
            {
                // Podemos implementar alguna lógica
                // para indicar que el proveedor no existe
                SupplierExceptions.ThrowInvalidSupplierIdException(id);
            }
            return Result;
        }

        public async Task<List<Supplier>> RetrieveAllAsync()
        {
            List<Supplier> Result = null;

            using (var r = RepositoryFactory.CreateRepository())
            {
                // Define el criterio de filtro para obtener todos los proveedores.
                Expression<Func<Supplier, bool>> allSuppliersCriteria = x => true;
                Result = await r.FilterAsync<Supplier>(allSuppliersCriteria);
            }
            return Result;
        }
    }
}
