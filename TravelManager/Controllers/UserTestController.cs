using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelManager.Models;

namespace TravelManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTestController : ControllerBase
    {
        // GET: api/UserTest
       

        private readonly UserManager<UserIdentity> _userManager;
        private readonly TravelManagerContext _context;

        public UserTestController(UserManager<UserIdentity> userManager, TravelManagerContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Authorize]
        [HttpGet]
         public async Task<ActionResult<string[]>> Get()
        {
            string userName1 = HttpContext.User.Claims.ToAsyncEnumerable().ElementAt(0).ToString();
            string userName2 = HttpContext.User.Claims.ToAsyncEnumerable().ElementAt(0).Id.ToString();
            string userName3 = HttpContext.User.Claims.ToAsyncEnumerable().ElementAt(0).Result.Value;
            string userName4 = HttpContext.User.Claims.ToAsyncEnumerable().ElementAt(0).Result.ToString();


            //_userManager.FindByNameAsync(HttpContext.User.Claims.ToAsyncEnumerable().ElementAt(0).ToString());

            return new string[] { userName1, userName2, userName3, userName4, "value1", "value2" };
        }

        // GET: api/UserTest/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/UserTest
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/UserTest/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            //return  BadRequest(new { error = "problem" });
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
