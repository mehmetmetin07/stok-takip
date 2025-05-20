using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class Product
    {
        public Product()
        {
            StockMovements = new HashSet<StockMovement>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Barcode { get; set; }

        public string? Description { get; set; }

        public int CategoryId { get; set; }

        public int? BrandId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        public int MinimumStockLevel { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxRate { get; set; } = 18; // Varsayılan KDV oranı %18

        public virtual Category? Category { get; set; }

        public virtual Brand? Brand { get; set; }

        public virtual ICollection<StockMovement> StockMovements { get; set; }
    }
} 