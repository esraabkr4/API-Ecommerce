using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        #region For EmailExist
        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            return Ok(await serviceManager.AuthenticationService.CheckEmailExist(email));
        }
        #endregion

        #region GetUserByEmail
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserResultDto>> GetUserByEmail()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user=await serviceManager.AuthenticationService.GetUserByEmail(email);
            return Ok(user);
        }
        #endregion

        #region GetUserAddress
        [HttpGet("Address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address=await serviceManager.AuthenticationService.GetUserAddress(email);
            return Ok(address);

        }
        #endregion

        #region Update User Address
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateUserAddress(AddressDto address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result= await serviceManager.AuthenticationService.UpdateUserAddress(address,email);
            return Ok(result);
        }
        #endregion
    }
}
