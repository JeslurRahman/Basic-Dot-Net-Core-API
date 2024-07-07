using BasicDotNetCoreAPI.Data;
using BasicDotNetCoreAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicDotNetCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly CustomerDbContext _customerDbContext;

        public CustomerController(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        public static List<Customer> customers = new()
        {
            new Customer { Id = 1, Name="Jeslur", City="Colombo", Country="Sri Lanaka"},
            new Customer { Id = 1, Name="xxxx", City="Jaffna", Country="Sri Lanaka"},
            new Customer { Id = 1, Name="yyyy", City="Matara", Country="Sri Lanaka"}

        };

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            #region Before Db Connection configuration
            //return Ok(customers);
            #endregion

            var customers = await _customerDbContext.Customers.ToListAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            #region Before Db Connection configuration
            /*var customer = customers.Find(c => c.Id == id);
            if(customer == null)
            {
                return NotFound("Customer not found");
            }
            return Ok(customer);*/
            #endregion

            var customer = await _customerDbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }
            return Ok(customer);

        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            #region Before Db Connection configuration
            /*customers.Add(customer);
            return Ok(customers);*/
            #endregion

            await _customerDbContext.Customers.AddAsync(customer);
            _customerDbContext.SaveChanges();
            return Ok(customer);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(Customer customer) 
        {
            #region Before Db Connection configuration
            /*var customerbyid = customers.Find(c => c.Id ==  customer.Id);
            
            if(customerbyid == null)
            {
                return NotFound("Invalid Customer details");
            }
            customerbyid.Name = customer.Name;
            customerbyid.City = customer.City;
            customerbyid.Country = customer.Country;

            return Ok(customerbyid);*/
            #endregion

            var customerById = await _customerDbContext.Customers.FirstOrDefaultAsync(c => c.Id == customer.Id);

            if (customerById == null)
            {
                return NotFound("Invalid Customer details");
            }

            customerById.Name = customer.Name;
            customerById.City = customer.City;
            customerById.Country = customer.Country;

            await _customerDbContext.SaveChangesAsync();
            return Ok(customerById);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id) 
        {
            #region Before Db Connection configuration
            /*var customer = customers.Find(c => c.Id == id);
            if (customer == null)
            {
                return NotFound("Invalid Customer details");
            }
            customers.Remove(customer);
            return Ok(customers);*/
            #endregion

            var customerById = await _customerDbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);

            if (customerById == null)
            {
                return NotFound("Invalid Customer details");
            }

             _customerDbContext.Customers.Remove(customerById);

            await _customerDbContext.SaveChangesAsync();
            return Ok(customerById);

        }
    }
}
