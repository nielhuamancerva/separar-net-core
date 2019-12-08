using System.Collections.Generic;
using Accounts.Application;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        //private readonly IAccountApplicationService _accountApplicationService;

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/accounts/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/accounts
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/accounts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/accounts/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
