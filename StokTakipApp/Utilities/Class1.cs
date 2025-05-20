using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Entities;

namespace Utilities
{
    public class ExcelExporter
    {
        public static void ExportProductsToExcel(List<Product> products, string filePath)
        {
            // Bu metot, EPPlus veya ClosedXML gibi kütüphaneler kullanılarak
            // ürün listesini Excel'e aktarmak için geliştirilebilir.
            
            throw new NotImplementedException("Bu özellik henüz uygulanmadı. Excel aktarımı için EPPlus veya ClosedXML kullanılabilir.");
        }
        
        public static void ExportStockMovementsToExcel(List<StockMovement> stockMovements, string filePath)
        {
            // Bu metot, EPPlus veya ClosedXML gibi kütüphaneler kullanılarak
            // stok hareketlerini Excel'e aktarmak için geliştirilebilir.
            
            throw new NotImplementedException("Bu özellik henüz uygulanmadı. Excel aktarımı için EPPlus veya ClosedXML kullanılabilir.");
        }
    }
}
