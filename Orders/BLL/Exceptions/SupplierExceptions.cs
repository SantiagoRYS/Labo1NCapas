using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class SupplierExceptions : Exception
    {
        private SupplierExceptions(string message) : base(message)
        {
            //Opcional: agregar constructo logico para logueo o manejo de errores del cliente
        }


        public static void ThrowSupplierAlreadyExistException(string CompanyName)
        {
            throw new SupplierExceptions($"A supplier with the name already exist {CompanyName}");
        }

        public static void ThrowInvalidSupplierDataException(string message)
        {
            throw new SupplierExceptions(message);
        }

        public static void ThrowInvalidSupplierIdException(int id)
        {
            throw new SupplierExceptions($"El ID del proveedor '{id}' no es válido.");
        }

    }
}

