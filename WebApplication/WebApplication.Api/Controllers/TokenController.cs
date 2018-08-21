using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Services.AuthenticationService;
using WebApplication.Services.Models.Requests;

namespace WebApplication.Api.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private IAuthenticationService authenticationService;

        public TokenController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = authenticationService.Authenticate(login.Username, login.Password);
            if (user != null)
            {
                var tokenString = authenticationService.BuildToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }
    }
}
