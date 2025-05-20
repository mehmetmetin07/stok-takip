using System;
using System.Collections.Generic;
using Entities;

namespace BLL.Services.Interfaces
{
    public interface ISaleService
    {
        List<Sale> GetAllSales();
        List<Sale> GetSalesByDateRange(DateTime startDate, DateTime endDate);
        List<Sale> GetSalesByCustomer(int customerId);
        List<Sale> GetSalesByUser(int userId);
        Sale GetSaleById(int id);
        Sale GetSaleByInvoiceNo(string invoiceNo);
        List<SaleItem> GetSaleItems(int saleId);
        
        // Satış işlemleri
        Sale CreateSale(Sale sale, List<SaleItem> saleItems);
        void UpdateSale(Sale sale);
        void CancelSale(int saleId, string cancellationReason);
        
        // Rapor ve özet işlemleri
        decimal GetTotalSaleAmount(DateTime startDate, DateTime endDate);
        int GetTotalSaleCount(DateTime startDate, DateTime endDate);
        List<SaleReportItem> GetSaleReport(DateTime startDate, DateTime endDate);
    }
    
    // Satış raporlaması için yardımcı sınıf
    public class SaleReportItem
    {
        public DateTime Date { get; set; }
        public int TotalSales { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetAmount { get; set; }
    }
} 