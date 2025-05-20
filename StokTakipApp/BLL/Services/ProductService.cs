using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Services.Interfaces;
using DAL.Repositories;
using Entities;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<StockMovement> _stockMovementRepository;

        public ProductService(IRepository<Product> productRepository, IRepository<StockMovement> stockMovementRepository)
        {
            _productRepository = productRepository;
            _stockMovementRepository = stockMovementRepository;
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAll().ToList();
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetById(id);
        }

        public Product GetProductByBarcode(string barcode)
        {
            return _productRepository.Find(p => p.Barcode == barcode).FirstOrDefault();
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _productRepository.Find(p => p.CategoryId == categoryId).ToList();
        }

        public List<Product> GetProductsByBrand(int brandId)
        {
            return _productRepository.Find(p => p.BrandId == brandId).ToList();
        }

        public List<Product> GetLowStockProducts()
        {
            return _productRepository.Find(p => p.StockQuantity <= p.MinimumStockLevel).ToList();
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<Product>();

            searchTerm = searchTerm.ToLower();
            return _productRepository.Find(p => 
                p.Name.ToLower().Contains(searchTerm) || 
                p.Barcode.ToLower().Contains(searchTerm) || 
                (p.Description != null && p.Description.ToLower().Contains(searchTerm))
            ).ToList();
        }

        public void AddProduct(Product product)
        {
            _productRepository.Add(product);
            _productRepository.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.Update(product);
            _productRepository.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _productRepository.GetById(id);
            if (product != null)
            {
                _productRepository.Remove(product);
                _productRepository.SaveChanges();
            }
        }

        public bool IsBarcodeUnique(string barcode, int? excludeProductId = null)
        {
            if (excludeProductId.HasValue)
                return !_productRepository.Find(p => p.Barcode == barcode && p.Id != excludeProductId.Value).Any();
            
            return !_productRepository.Find(p => p.Barcode == barcode).Any();
        }

        public void UpdateStock(int productId, int quantity, MovementType movementType, int? userId, string description = null)
        {
            var product = _productRepository.GetById(productId);
            if (product == null)
                throw new Exception("Ürün bulunamadı.");

            if (movementType == MovementType.StockOut && quantity > product.StockQuantity)
                throw new Exception("Çıkış yapılmak istenen miktar stokta mevcut değil.");

            // Stok miktarını güncelle
            if (movementType == MovementType.StockIn)
                product.StockQuantity += quantity;
            else
                product.StockQuantity -= quantity;

            // Stok hareketini kaydet
            var stockMovement = new StockMovement
            {
                ProductId = productId,
                Quantity = quantity,
                MovementType = movementType,
                MovementDate = DateTime.Now,
                Description = description,
                UserId = userId
            };

            _stockMovementRepository.Add(stockMovement);
            _productRepository.Update(product);
            _productRepository.SaveChanges();
        }
    }
} 