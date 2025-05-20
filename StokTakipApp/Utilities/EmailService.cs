using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using Entities;

namespace Utilities
{
    public class EmailService
    {
        // SMTP ayarları
        private string SmtpHost { get; set; }
        private int SmtpPort { get; set; }
        private string SmtpUsername { get; set; }
        private string SmtpPassword { get; set; }
        private bool EnableSsl { get; set; }
        
        public EmailService(string smtpHost, int smtpPort, string smtpUsername, string smtpPassword, bool enableSsl = true)
        {
            SmtpHost = smtpHost;
            SmtpPort = smtpPort;
            SmtpUsername = smtpUsername;
            SmtpPassword = smtpPassword;
            EnableSsl = enableSsl;
        }
        
        public bool SendEmail(string to, string subject, string body, bool isHtml = true)
        {
            try
            {
                using (var client = new SmtpClient(SmtpHost, SmtpPort))
                {
                    client.EnableSsl = EnableSsl;
                    client.Credentials = new NetworkCredential(SmtpUsername, SmtpPassword);
                    
                    using (var message = new MailMessage(SmtpUsername, to, subject, body))
                    {
                        message.IsBodyHtml = isHtml;
                        client.Send(message);
                    }
                }
                
                return true;
            }
            catch (Exception ex)
            {
                // Gerçek uygulamada hata loglama yapılmalı
                Console.WriteLine($"E-posta gönderme hatası: {ex.Message}");
                return false;
            }
        }
        
        public bool SendLowStockAlert(List<Product> lowStockProducts, string adminEmail)
        {
            if (lowStockProducts == null || lowStockProducts.Count == 0)
                return false;
                
            string subject = "Stok Seviyesi Düşük Ürünler Bildirimi";
            
            // HTML e-posta içeriği oluştur
            string body = "<h2>Stok Seviyesi Düşük Ürünler</h2>";
            body += "<p>Aşağıdaki ürünlerin stok seviyesi minimum değerin altına düşmüştür:</p>";
            body += "<table border='1' cellpadding='5'>";
            body += "<tr><th>Ürün Kodu</th><th>Ürün Adı</th><th>Mevcut Stok</th><th>Minimum Stok</th></tr>";
            
            foreach (var product in lowStockProducts)
            {
                body += $"<tr><td>{product.Barcode}</td><td>{product.Name}</td><td>{product.StockQuantity}</td><td>{product.MinimumStockLevel}</td></tr>";
            }
            
            body += "</table>";
            body += "<p>Lütfen bu ürünleri en kısa sürede tedarik ediniz.</p>";
            
            return SendEmail(adminEmail, subject, body);
        }
    }
} 