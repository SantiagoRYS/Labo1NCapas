// See https://aka.ms/new-console-template for more information
using DAL;
using Entities.Models;
using System.Linq.Expressions;

CreateAsync().GetAwaiter().GetResult();
RetreiveAsync().GetAwaiter().GetResult();

static async Task CreateAsync()
{
    //Add Customer 
    Customer customer = new Customer()
    {
        FirstName = "Santiago",
        LastName = "Gonzalez",
        City = "Bogota",
        Country = "Colombia",
        Phone = "3158964587"
    };
    using (var repository = RepositoryFactory.CreateRepository())
    {
        try
        {
            var createdCustomer = await repository.CreateAsync(customer);
            Console.WriteLine($"Added Customer: {createdCustomer.LastName} {createdCustomer.FirstName}");
        }
        catch (Exception Ex)
        {

            Console.WriteLine($"Error: {Ex.Message}");
        }
    }
}

static async Task RetreiveAsync()
{
    using (var repository = RepositoryFactory.CreateRepository())
    {
        try
        {
            Expression<Func<Customer, bool>> criteria = c => c.FirstName == "Santiago" && c.LastName == "Gonzalez";
            var Customer = await repository.RetreiveAsync(criteria);
            if (Customer != null)
            {
                Console.WriteLine($"Retrived customer: {Customer.FirstName} \t {Customer.LastName} \t City: {Customer.City} \t Country: {Customer.Country}");
            }
            else
            {
                Console.WriteLine($"Customer doesn't exist");
            }
        }
        catch (Exception Ex)
        {

            Console.WriteLine($"Error: {Ex.Message}");
        }
    }
}