using System;
using System.ComponentModel.DataAnnotations;

namespace Harvestly.Models
{
    public enum UserRole
    {
        Admin = 1,
        Farmer = 2
    }

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        public string Username { get; set; }

        [StringLength(255)]
        public string PasswordHash { get; set; }

        [Required]
        public UserRole Role { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }
    }
}

