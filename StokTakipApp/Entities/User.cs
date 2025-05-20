using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public enum UserRole
    {
        Admin = 1,
        User = 2
    }

    public class User
    {
        public User()
        {
            StockMovements = new HashSet<StockMovement>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [Required]
        public UserRole Role { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public virtual ICollection<StockMovement> StockMovements { get; set; }
    }
} 