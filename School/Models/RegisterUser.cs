using System.ComponentModel.DataAnnotations;

namespace School.Models
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Password is required")]
        public string Email { get; set; }
    }
}
