using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace SpyVK.ViewModels
{
    public class SignInApplicationUser
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public IEnumerable<AuthenticationScheme> ExternalProviders { get; set; }
    }
}
