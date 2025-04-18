using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Security
{
    public record RegisterDto
    {
        [Required(ErrorMessage = "DisplayName Is Required !!!")]
        public string DisplayName { get; init; }
        [Required(ErrorMessage = "Email Address Is Required !!!")]
        [EmailAddress]
        public string Email { get; init; }
        [Required(ErrorMessage = "Password Address Is Required !!!")]
        public string Password { get; init; }
        [Required(ErrorMessage = "User Name Is Required !!!")]
        public string UserName { get; init; }
        public string? PhoneNumber { get; init; }

    }
}
