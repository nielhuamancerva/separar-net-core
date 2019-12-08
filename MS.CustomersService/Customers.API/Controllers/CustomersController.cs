using System.Collections.Generic;
using Customers.Application;
using Microsoft.AspNetCore.Mvc;

namespace Customers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        //private readonly ICustomerApplicationService _customerApplicationService;

        // GET api/customers
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/customers/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/customers
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/customers/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
