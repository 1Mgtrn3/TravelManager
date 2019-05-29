using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelManager.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TravelManager.Auth;
using TravelManager.Helpers;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace TravelManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly TravelManagerContext _context;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        //public static void Initialize(IServiceProvider serviceProvider)
        //{

        //}
        public AuthController(UserManager<UserIdentity> userManager, TravelManagerContext context, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _context = context;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

             
        // GET: api/Auth
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Auth/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Auth
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] AuthData authData)
        {
            UserIdentity userIdentity = new UserIdentity()
            {
                Email = authData.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = authData.Username
            };
            var result = await _userManager.CreateAsync(userIdentity, authData.Password);
            if (!result.Succeeded) return BadRequest();//BadRequestObjectResult(Errors.Add);
            //var identityId = userIdentity.I
            await _context.Users.AddAsync(new User { Username = userIdentity.UserName, Email = userIdentity.Email, CurrencyId = authData.CurrencyId, IdentityId = userIdentity.Id });
            await _context.SaveChangesAsync();
            return new OkObjectResult("Account created");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var identity = await GetClaimsIdentity(model.Username, model.Password);



            var user = await _userManager.FindByNameAsync(model.Username);
            if (identity == null)
            {
                return Unauthorized();
            }

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, model.Username, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }
        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userToVerify.UserName, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

        // PUT: api/Auth/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
