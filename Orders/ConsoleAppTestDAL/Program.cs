// See https://aka.ms/new-console-template for more information
using DAL;
using Entities.Models;
using System.Linq.Expressions;

//CreateAsync().GetAwaiter().GetResult();
//RetreiveAsync().GetAwaiter().GetResult();
UpdateAsync().GetAwaiter().GetResult();
//FilterAsync().GetAwaiter().GetResult();
//DeleteAsync().GetAwaiter().GetResult();

Console.ReadKey();

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
            var Customer = await repository.RetrieveAsync(criteria);
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

static async Task UpdateAsync()
{
    //Supuesto: Existe el objeto a modificar

    using (var repository = RepositoryFactory.CreateRepository())
    {
        var customerToUpdate = await repository.RetrieveAsync<Customer>(c => c.Id == 1);
        if (customerToUpdate != null)
        {
            customerToUpdate.City = "cucuta";
            customerToUpdate.Country = "Colombia";
            customerToUpdate.Phone = "+57 3145621864";
        }
        try
        {
            bool updated = await repository.UpdateAsync(customerToUpdate);
            if (updated)
            {
                Console.WriteLine("Customer updated successfully.");
            }
            else
            {
                Console.WriteLine("Customer update failed");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    
}

static async Task FilterAsync()
{
    using (var repository = RepositoryFactory.CreateRepository())
    {
        Expression<Func<Customer, bool>> criteria = c => c.Country == "Brazil";

        var customers = await repository.FilterAsync(criteria);

        foreach (var customer in customers)
        {
            Console.WriteLine($"Customer: {customer.FirstName} {customer.LastName}\t from {customer.City}");
        }
    }
}

static async Task DeleteAsync()
{
    using (var repository = RepositoryFactory.CreateRepository())
    {
        Expression<Func<Customer, bool>> criteria = c => c.Id == 93;

        var customerstoDelete = await repository.RetrieveAsync(criteria);

        if (customerstoDelete != null)
        {
            bool deleted = await repository.DeleteAsync(customerstoDelete);
            Console.WriteLine(deleted ? "Customer deleted succesfully." : "Failed to delete customer");
        }
    }
}