using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public enum MovementType
    {
        StockIn = 1,
        StockOut = 2
    }

    public class StockMovement
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public MovementType MovementType { get; set; }

        [Required]
        public DateTime MovementDate { get; set; }

        public string? Description { get; set; }

        public int? UserId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? UnitPrice { get; set; }

        public virtual Product? Product { get; set; }

        public virtual User? User { get; set; }
    }
} 