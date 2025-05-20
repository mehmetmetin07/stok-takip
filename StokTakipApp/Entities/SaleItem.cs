using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class SaleItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxRate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountRate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        // İlişkiler
        public int SaleId { get; set; }
        public virtual Sale Sale { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        // Birim fiyat ve toplam fiyatı hesaplama metodları
        public void CalculateTaxAmount()
        {
            TaxAmount = Math.Round(UnitPrice * Quantity * (TaxRate / 100), 2);
        }

        public void CalculateDiscountAmount()
        {
            DiscountAmount = Math.Round(UnitPrice * Quantity * (DiscountRate / 100), 2);
        }

        public void CalculateTotalAmount()
        {
            TotalAmount = Math.Round((UnitPrice * Quantity) + TaxAmount - DiscountAmount, 2);
        }
    }
} 