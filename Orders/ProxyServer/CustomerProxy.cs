using Entities.Models;
using ProxyServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyServer
{
    public class CustomerProxy : ICustomerProxy
    {
        private readonly HttpClient _httpClient;

        public CustomerProxy()
        {
            _httpClient = new HttpClient();
            BaseAddress = new Uri ("httpClient://localhost:7041/api/Customer/")
        }


        public Task<Customer> CreateAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Customer>> GetAllAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id, Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
