using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAppWebApi.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("user_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Column("username")]
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Username must be at least 5 characters.")]
        public string? Username { get; set; } 

        [Column("hashed_password")]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(200, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string HashedPassword { get; set; } = "";

        [Column("email")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(255, ErrorMessage = "Email must not exceed 255 characters.")]
        public string Email { get; set; } = "";

        [Column("phone")]
        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(20, ErrorMessage = "Phone number must not exceed 20 characters.")]
        [RegularExpression(@"^\+?[0-9]*$", ErrorMessage = "Invalid phone number format.")]
        public string? Phone { get; set; }

        [Column("full_name")]
        [StringLength(255, ErrorMessage = "Full name must not exceed 255 characters.")]
        public string? FullName { get; set; } 

        [Column("date_of_birth")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date of birth.")]
        public DateTime? DateOfBirth { get; set; }

        [Column("country")]
        [StringLength(200, ErrorMessage = "Country must not exceed 200 characters.")]
        public string? Country { get; set; } 

        //public User()
        //{
        //    // Set default values for nullable properties
        //    DateOfBirth ??= DateTime.Now; // Assuming you want the default value to be the current date if not provided
        //    FullName ??= string.Empty;
        //    Country ??= string.Empty;
        //}

        public ICollection<WatchList>? WatchLists { get; set; }
    }

}

