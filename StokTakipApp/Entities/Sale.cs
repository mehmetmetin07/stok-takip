using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Sale
    {
        public Sale()
        {
            SaleItems = new HashSet<SaleItem>();
            SaleDate = DateTime.Now;
            InvoiceNo = GenerateInvoiceNo();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string InvoiceNo { get; set; }

        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FinalAmount { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        public bool IsCancelled { get; set; }

        public DateTime? CancellationDate { get; set; }

        public string? CancellationReason { get; set; }

        // İlişkiler
        public int? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<SaleItem> SaleItems { get; set; }

        // Fatura numarası oluşturma - gerçek bir uygulamada daha karmaşık olabilir
        private string GenerateInvoiceNo()
        {
            string dateStr = DateTime.Now.ToString("yyyyMMdd");
            string timeStr = DateTime.Now.ToString("HHmmss");
            return $"INV{dateStr}{timeStr}";
        }
    }

    public enum PaymentMethod
    {
        Cash,
        CreditCard,
        DebitCard,
        Other
    }
} 