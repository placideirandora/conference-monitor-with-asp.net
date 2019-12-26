using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceMonitorApi.Models
{
    // Create and validate the model for users table
    [Table("Users")]
    public class User : IUser
    {   
        // Implement the interface properties
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(15, ErrorMessage = "First Name cannot be longer than 15 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(15, ErrorMessage = "Last Name cannot be longer than 15 characters")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email Address. (ex: someone@email.com)")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, ErrorMessage = "Password must be between 8 and 50 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^\S*(?=\S{8,})(?=\S*[a-z])(?=\S*[A-Z])(?=\S*[\d])(?=\S*[\W])\S*$", ErrorMessage = "Password must contain at least one uppercase letter (ex: A, B, etc.), one lowercase letter (ex: a, b, etc.), one special character (ex: $, #, @, !,%,^,&,*, etc.), and one digit number (ex: 0, 1, 2, 3, etc.)")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; } = "standard";

        public string Registered { get; set; } = DateTime.Now.ToLocalTime().ToString("F");
    }
}