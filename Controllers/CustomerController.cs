using BasicDotNetCoreAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasicDotNetCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public static List<Customer> customers = new()
        {
            new Customer { Id = 1, Name="Jeslur", City="Colombo", Country="Sri Lanaka"},
            new Customer { Id = 1, Name="xxxx", City="Jaffna", Country="Sri Lanaka"},
            new Customer { Id = 1, Name="yyyy", City="Matara", Country="Sri Lanaka"}

        };

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = customers.Find(c => c.Id == id);
            if(customer == null)
            {
                return NotFound("Customer not found");
            }
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            customers.Add(customer);
            return Ok(customers);
        }

        [HttpPut]
        public IActionResult UpdateCustomer(Customer customer) 
        {
            var customerbyid = customers.Find(c => c.Id ==  customer.Id);
            
            if(customerbyid == null)
            {
                return NotFound("Invalid Customer details");
            }
            customerbyid.Name = customer.Name;
            customerbyid.City = customer.City;
            customerbyid.Country = customer.Country;

            return Ok(customerbyid);
        }

        [HttpDelete]
        public IActionResult DeleteCustomer(int id) 
        {
            var customer = customers.Find(c => c.Id == id);
            if (customer == null)
            {
                return NotFound("Invalid Customer details");
            }
            customers.Remove(customer);
            return Ok(customers);

        }
    }
}
