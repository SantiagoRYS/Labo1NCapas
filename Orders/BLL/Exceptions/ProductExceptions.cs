using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class ProductExceptions : Exception
    {
        private ProductExceptions(string message) : base(message)
        {
            //Opcional: agregar constructo logico para logueo o manejo de errores del producto
        }


        public static void ThrowProductAlreadyExistException(string ProductName)
        {
            throw new ProductExceptions($"A product with the name already exist {ProductName}");
        }

        public static void ThrowInvalidProductDataException(string message)
        {
            throw new ProductExceptions(message);
        }

        public static void ThrowInvalidProductIdException(int id)
        {
            throw new ProductExceptions($"El ID del producto '{id}' no es válido.");
        }

    }
}

