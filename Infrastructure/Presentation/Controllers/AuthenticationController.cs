using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.Security;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager serviceManager):ApiController
    {
        #region Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDto>> Login(LoginDto loginDto)
        {
            if (loginDto != null)
            {
                var result= await serviceManager.AuthenticationService.LoginAsync(loginDto);
                return Ok(result);
            }
            return NoContent();
        }
        #endregion
        #region Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDto>> Register(RegisterDto registerDto)
        {
            if (registerDto != null)
            {
                var result = await serviceManager.AuthenticationService.RegisterAsync(registerDto);
                return Ok(result);
            }
            return NoContent();
        }
        #endregion
    }
}
