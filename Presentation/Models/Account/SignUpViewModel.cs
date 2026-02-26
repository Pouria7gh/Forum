using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.Account
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(maximumLength: 30, MinimumLength = 2, ErrorMessage = "{0} length must be between {2} and {1}.")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(maximumLength: 30, MinimumLength = 6, ErrorMessage = "{0} length must be between {2} and {1}.")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Dispaly name is required")]
        [StringLength(maximumLength: 30, MinimumLength = 2, ErrorMessage = "{0} length must be between {2} and {1}.")]
        public required string DisplayName { get; set; }
    }
}
