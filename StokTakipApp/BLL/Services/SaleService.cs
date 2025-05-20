using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Services.Interfaces;
using DAL.Repositories;
using Entities;

namespace BLL.Services
{
    public class SaleService : ISaleService
    {
        private readonly IRepository<Sale> _saleRepository;
        private readonly IRepository<SaleItem> _saleItemRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IProductService _productService;

        public SaleService(
            IRepository<Sale> saleRepository,
            IRepository<SaleItem> saleItemRepository,
            IRepository<Product> productRepository,
            IProductService productService)
        {
            _saleRepository = saleRepository;
            _saleItemRepository = saleItemRepository;
            _productRepository = productRepository;
            _productService = productService;
        }

        public List<Sale> GetAllSales()
        {
            return _saleRepository.GetAll().ToList();
        }

        public List<Sale> GetSalesByDateRange(DateTime startDate, DateTime endDate)
        {
            // Bitiş tarihini günün sonuna ayarla
            endDate = endDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            
            return _saleRepository.Find(s => 
                s.SaleDate >= startDate && 
                s.SaleDate <= endDate).ToList();
        }

        public List<Sale> GetSalesByCustomer(int customerId)
        {
            return _saleRepository.Find(s => s.CustomerId == customerId).ToList();
        }

        public List<Sale> GetSalesByUser(int userId)
        {
            return _saleRepository.Find(s => s.UserId == userId).ToList();
        }

        public Sale GetSaleById(int id)
        {
            return _saleRepository.GetById(id);
        }

        public Sale GetSaleByInvoiceNo(string invoiceNo)
        {
            return _saleRepository.Find(s => s.InvoiceNo == invoiceNo).FirstOrDefault();
        }

        public List<SaleItem> GetSaleItems(int saleId)
        {
            return _saleItemRepository.Find(si => si.SaleId == saleId).ToList();
        }

        public Sale CreateSale(Sale sale, List<SaleItem> saleItems)
        {
            try
            {
                // Satış ve satış detayların birbiriyle ilişkilendirilmesi
                foreach (var item in saleItems)
                {
                    item.SaleId = sale.Id;
                    
                    // Ürün stoğunu güncelle
                    _productService.UpdateStock(
                        item.ProductId, 
                        item.Quantity, 
                        MovementType.StockOut, 
                        sale.UserId, 
                        $"Satış-{sale.InvoiceNo}"
                    );
                }
                
                // Toplam tutarları hesapla
                CalculateSaleTotals(sale, saleItems);
                
                // Satışı kaydet
                _saleRepository.Add(sale);
                _saleRepository.SaveChanges();
                
                // Satış detaylarını kaydet
                foreach (var item in saleItems)
                {
                    _saleItemRepository.Add(item);
                }
                _saleItemRepository.SaveChanges();
                
                return sale;
            }
            catch (Exception ex)
            {
                throw new Exception($"Satış oluşturulurken hata oluştu: {ex.Message}", ex);
            }
        }

        public void UpdateSale(Sale sale)
        {
            _saleRepository.Update(sale);
            _saleRepository.SaveChanges();
        }

        public void CancelSale(int saleId, string cancellationReason)
        {
            var sale = _saleRepository.GetById(saleId);
            if (sale == null)
                throw new Exception("Satış bulunamadı.");
            
            if (sale.IsCancelled)
                throw new Exception("Bu satış zaten iptal edilmiş.");
            
            // Satışı iptal et
            sale.IsCancelled = true;
            sale.CancellationDate = DateTime.Now;
            sale.CancellationReason = cancellationReason;
            
            // Stok güncelleme - ürünleri stoğa geri ekle
            var saleItems = GetSaleItems(saleId);
            foreach (var item in saleItems)
            {
                _productService.UpdateStock(
                    item.ProductId, 
                    item.Quantity, 
                    MovementType.StockIn, 
                    sale.UserId, 
                    $"İptal-{sale.InvoiceNo}"
                );
            }
            
            // Değişiklikleri kaydet
            _saleRepository.SaveChanges();
        }

        public decimal GetTotalSaleAmount(DateTime startDate, DateTime endDate)
        {
            // Bitiş tarihini günün sonuna ayarla
            endDate = endDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            
            return _saleRepository.Find(s => 
                !s.IsCancelled && 
                s.SaleDate >= startDate && 
                s.SaleDate <= endDate)
                .Sum(s => s.FinalAmount);
        }

        public int GetTotalSaleCount(DateTime startDate, DateTime endDate)
        {
            // Bitiş tarihini günün sonuna ayarla
            endDate = endDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            
            return _saleRepository.Find(s => 
                !s.IsCancelled && 
                s.SaleDate >= startDate && 
                s.SaleDate <= endDate).Count();
        }

        public List<SaleReportItem> GetSaleReport(DateTime startDate, DateTime endDate)
        {
            // Bitiş tarihini günün sonuna ayarla
            endDate = endDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            
            var sales = _saleRepository.Find(s => 
                !s.IsCancelled && 
                s.SaleDate >= startDate && 
                s.SaleDate <= endDate).ToList();
                
            var report = new List<SaleReportItem>();
            
            // Günlük bazda rapor oluştur
            var groupedSales = sales.GroupBy(s => s.SaleDate.Date);
            foreach (var group in groupedSales)
            {
                var reportItem = new SaleReportItem
                {
                    Date = group.Key,
                    TotalSales = group.Count(),
                    TotalAmount = group.Sum(s => s.TotalAmount),
                    TotalTax = group.Sum(s => s.TaxAmount),
                    TotalDiscount = group.Sum(s => s.DiscountAmount),
                    NetAmount = group.Sum(s => s.FinalAmount)
                };
                
                report.Add(reportItem);
            }
            
            return report.OrderBy(r => r.Date).ToList();
        }
        
        // Yardımcı metodlar
        private void CalculateSaleTotals(Sale sale, List<SaleItem> saleItems)
        {
            decimal totalAmount = 0;
            decimal taxAmount = 0;
            decimal discountAmount = 0;
            
            foreach (var item in saleItems)
            {
                // Her bir satır için hesaplama
                item.CalculateTaxAmount();
                item.CalculateDiscountAmount();
                item.CalculateTotalAmount();
                
                // Genel toplamlara ekle
                totalAmount += item.UnitPrice * item.Quantity;
                taxAmount += item.TaxAmount;
                discountAmount += item.DiscountAmount;
            }
            
            // Satış toplamlarını güncelle
            sale.TotalAmount = totalAmount;
            sale.TaxAmount = taxAmount;
            sale.DiscountAmount = discountAmount;
            sale.FinalAmount = totalAmount + taxAmount - discountAmount;
        }
    }
} 