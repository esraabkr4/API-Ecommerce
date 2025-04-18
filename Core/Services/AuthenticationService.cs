using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction;
using Shared.Security;

namespace Services
{
    public class AuthenticationService(UserManager<User> userManager,IConfiguration configuration,IOptions<JwtOptions> options) : IAuthenticationService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user=await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) throw new UnauthorizedException("Invalid Email");
            var result=await userManager.CheckPasswordAsync(user, loginDto.Password);
            if(!result)throw new UnauthorizedException("Incorrect Password");
            return new UserResultDto(
                user.DisplayName,
                user.Email,
                await CreateTokenAsync(user)
            );
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName
            };
            var result=await userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new RegisterValidationException(errors);
            }
            return new UserResultDto(
                user.DisplayName, 
                user.Email,
                await CreateTokenAsync(user)
                );
        }
    
    public async Task<string> CreateTokenAsync(User user)
        {
            var JwtOptions = options.Value;
            var AuthClaim = new List<Claim>
            {
                (new Claim(ClaimTypes.Name,user.UserName)!),
                (new Claim(ClaimTypes.Email,user.Email)!)
            };
       var roles=await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                AuthClaim.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecretKey));
            var SigningCredintial=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token=new JwtSecurityToken(
                audience:JwtOptions.Audience,
                issuer: JwtOptions.Issure,
                expires:DateTime.UtcNow.AddDays(JwtOptions.DurationInDays),
                claims:AuthClaim,
                signingCredentials:SigningCredintial);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
