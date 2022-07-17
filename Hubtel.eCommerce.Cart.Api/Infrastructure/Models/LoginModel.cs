using System.ComponentModel.DataAnnotations;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }

    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";
    }
}
