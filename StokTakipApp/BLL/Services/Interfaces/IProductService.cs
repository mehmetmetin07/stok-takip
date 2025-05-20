using System;
using System.Collections.Generic;
using Entities;

namespace BLL.Services.Interfaces
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        Product GetProductByBarcode(string barcode);
        List<Product> GetProductsByCategory(int categoryId);
        List<Product> GetProductsByBrand(int brandId);
        List<Product> GetLowStockProducts();
        List<Product> SearchProducts(string searchTerm);
        
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        
        bool IsBarcodeUnique(string barcode, int? excludeProductId = null);
        void UpdateStock(int productId, int quantity, MovementType movementType, int? userId, string description = null);
    }
} 