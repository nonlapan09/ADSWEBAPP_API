using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace ADSWEBAPP_API.Dto.AuthenData
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }

    public class SignUpUser
    {
        [DefaultValue("Input username lowercase or numbers")]
        [Required(ErrorMessage = "User Name is required")]
        [RegularExpression("^[a-z0-9]*$", ErrorMessage = "Should be lowercase with numbers")]
        public string? Username { get; set; }

        [DefaultValue("Input password at least 8 characters")]
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^.{8,}$", ErrorMessage = "Should be Password at least 8 characters")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        public string? Firstname { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        public string? Lastname { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DefaultValue("Input Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }
    }
}
