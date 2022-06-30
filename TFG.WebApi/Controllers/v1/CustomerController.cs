using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TFG.WebApi.Models;

namespace TFG.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v1/customers")]
    public class CustomerController : ControllerBase
    {

        public List<Customer> customers = new()
        {
            new Customer()
            {
                CustomerId = 1,
                Name = "Customer 1",
                Description = "Customer 1 is the first customer.",
                Location = "1 Green road, Gauteng",
                EmailAddress = "customer1@customer.co.za",
                ContactNumber = "0845332346"
            },
            new Customer()
            {
                CustomerId = 2,
                Name = "Customer 2",
                Description = "Customer 2 is the second customer.",
                Location = "134 Sydney Road, Cape Town",
                EmailAddress = "customer2@customer.co.za",
                ContactNumber = "0847777777"
            },
            new Customer()
            {
                CustomerId = 3,
                Name = "Customer 3",
                Description = "Customer 3 is the third customer.",
                Location = "456 Obedtory Road, Limpopo",
                EmailAddress = "customer3@customer.co.za",
                ContactNumber = "0868993394"
            }
        };


        public CustomerController()
        { }


        [HttpGet]
        public List<Customer> Get()
        {
            return customers;
        }

        [HttpGet]
        [Route("{Id}")]
        public Customer GetById(int Id)
        {
            return customers.Where(x => x.CustomerId == Id).FirstOrDefault();
        }

        [HttpGet]
        [Route("search/{searchText}")]
        public List<Customer> SearchForCustomer(string searchText)
        {
            var filteredCustomers = (from cus in customers
                                 where cus.CustomerId.ToString().Contains(searchText) ||
                                 cus.Description.Contains(searchText) ||
                                 cus.EmailAddress.Contains(searchText) ||
                                 cus.Location.Contains(searchText) ||
                                 cus.Name.Contains(searchText) ||
                                 cus.ContactNumber.Contains(searchText)
                                 select cus).ToList();

            return filteredCustomers;
        }

        [HttpDelete]
        [Route("{Id}")]
        public bool Delete(int Id)
        {
            var existingItem = customers.Where(x => x.CustomerId == Id).FirstOrDefault();

            if (existingItem == null) return false;

            customers.Remove(existingItem);

            return true;
        }
    }
}
