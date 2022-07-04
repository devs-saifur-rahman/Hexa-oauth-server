using System.ComponentModel.DataAnnotations;

namespace Hexa.Data.DTOs
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Must be between 4 and 50 characters", MinimumLength = 4)]

        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [StringLength(16, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(36, ErrorMessage = "Must be between 8 and 36 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(36, ErrorMessage = "Must be between 8 and 36 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
                                                                                                                                                                                               