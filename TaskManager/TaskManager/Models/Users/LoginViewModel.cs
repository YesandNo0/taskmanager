using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models {
    public class LoginViewModel {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "The field {0} is required.")]
        [EmailAddress(ErrorMessage = ".")]
        public string Email { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "The field {0} is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "The {0} field must have at least {1} characters.")]
        public string Password { get; set; }

        [Display(Name = "Remember me in this browser")]
        public bool RememberMe { get; set; }
    }
}