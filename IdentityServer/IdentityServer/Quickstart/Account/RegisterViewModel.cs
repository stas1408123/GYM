using System.ComponentModel.DataAnnotations;

namespace IdentityServerHost.Quickstart.UI
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public int Year { get; set; } = 2000;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password Should be identical")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        public string? ReturnUrl { get; set; }
    }
}

