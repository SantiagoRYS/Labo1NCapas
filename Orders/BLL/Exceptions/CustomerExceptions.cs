using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class CustomerExceptions : Exception
    {

        private CustomerExceptions(string message) : base(message)
        {
            //Opcional: agregar constructo logico para logueo o manejo de errores del cliente
        }


        public static void ThrowCustomerAlreadyExistException(string firstName, string lastName)
        {
            throw new CustomerExceptions($"A client with the name already exist {firstName} {lastName}");
        }

        public static void ThrowInvalidCustomerDataException(string message)
        {
            throw new CustomerExceptions(message);
        }

        public static void ThrowInvalidCustomerIdException(int id)
        {
            throw new CustomerExceptions($"El ID del cliente '{id}' no es válido.");
        }

    }
}
