using BasicDotNetCoreAPI.Data;
using BasicDotNetCoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicDotNetCoreAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerDbContext _customerDbContext;

        public CustomerService(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            var customers = await _customerDbContext.Customers.ToListAsync();
            return customers;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerDbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
            return customer;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            await _customerDbContext.Customers.AddAsync(customer);
            _customerDbContext.SaveChanges();
            return customer;
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            var customerById = await _customerDbContext.Customers.FirstOrDefaultAsync(c => c.Id == customer.Id);

            if(customerById != null)
            {
                customerById.Name = customer.Name;
                customerById.City = customer.City;
                customerById.Country = customer.Country;
            }
          
            await _customerDbContext.SaveChangesAsync();
            return customerById;
        }

        public async Task<Customer> DeleteCustomerAsync(int id)
        {
            var customerById = await _customerDbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customerById != null)
            {
                _customerDbContext.Customers.Remove(customerById);
            }

            await _customerDbContext.SaveChangesAsync();
            return customerById;
        }
    }
}
