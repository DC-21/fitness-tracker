using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Fit.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "User name is required.")]
        [StringLength(100, ErrorMessage = "User name can't be longer than 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "User name can only contain letters and numbers.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Password must be exactly 12 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z]).+$", ErrorMessage = "Password must contain at least one uppercase and one lowercase letter.")]
        public string Password { get; set; }

        public ICollection<Goal> Goals { get; set; }

        // Collections for each activity type
        public ICollection<Walking> Walkings { get; set; }
        public ICollection<Cycling> Cyclings { get; set; }
        public ICollection<Running> Runnings { get; set; }
        public ICollection<Swimming> Swimmings { get; set; }
        public ICollection<Weights> Weights { get; set; }
        public ICollection<Hiking> Hikings { get; set; }
    }
}
