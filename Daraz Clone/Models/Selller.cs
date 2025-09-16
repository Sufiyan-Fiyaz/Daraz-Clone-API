using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Daraz_Clone.Models
{
    public class Seller
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [MaxLength(200)]
        public string ShopName { get; set; }

        [MaxLength(200)]
        public string? BusinessLicense { get; set; }

        [MaxLength(100)]
        public string? BankAccount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        //  Navigation property (One-to-One with User)
        public User User { get; set; }
    }
}
