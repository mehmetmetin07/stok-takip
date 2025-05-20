using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Services.Interfaces;
using DAL.Repositories;
using Entities;

namespace BLL.Services
{
    public class StockService : IStockService
    {
        private readonly IRepository<StockMovement> _stockMovementRepository;
        private readonly IRepository<Product> _productRepository;

        public StockService(IRepository<StockMovement> stockMovementRepository, IRepository<Product> productRepository)
        {
            _stockMovementRepository = stockMovementRepository;
            _productRepository = productRepository;
        }

        public List<StockMovement> GetAllStockMovements()
        {
            return _stockMovementRepository.GetAll().ToList();
        }

        public StockMovement GetStockMovementById(int id)
        {
            return _stockMovementRepository.GetById(id);
        }

        public List<StockMovement> GetStockMovementsByProduct(int productId)
        {
            return _stockMovementRepository.Find(sm => sm.ProductId == productId).ToList();
        }

        public List<StockMovement> GetStockMovementsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _stockMovementRepository.Find(sm => 
                sm.MovementDate >= startDate && 
                sm.MovementDate <= endDate).ToList();
        }

        public List<StockMovement> GetStockMovementsByUser(int userId)
        {
            return _stockMovementRepository.Find(sm => sm.UserId == userId).ToList();
        }

        public void AddStockMovement(StockMovement stockMovement)
        {
            if (stockMovement == null)
                throw new ArgumentNullException(nameof(stockMovement));

            // Hareket tipi ve miktar kontrolü
            if (stockMovement.Quantity <= 0)
                throw new ArgumentException("Miktar sıfırdan büyük olmalıdır.");

            // Ürün kontrolü
            var product = _productRepository.GetById(stockMovement.ProductId);
            if (product == null)
                throw new ArgumentException("Ürün bulunamadı.");

            // Hareket tipi çıkış ise stok kontrolü
            if (stockMovement.MovementType == MovementType.StockOut && stockMovement.Quantity > product.StockQuantity)
                throw new InvalidOperationException($"Yetersiz stok. Mevcut stok: {product.StockQuantity}");

            // Stok miktarını güncelle
            if (stockMovement.MovementType == MovementType.StockIn)
                product.StockQuantity += stockMovement.Quantity;
            else
                product.StockQuantity -= stockMovement.Quantity;

            // Hareket tarihi belirtilmemişse şu an
            if (stockMovement.MovementDate == DateTime.MinValue)
                stockMovement.MovementDate = DateTime.Now;

            // Stok hareketini kaydet
            _stockMovementRepository.Add(stockMovement);
            _productRepository.Update(product);
            _stockMovementRepository.SaveChanges();
        }

        public void UpdateProductStock(int productId, int quantity, MovementType movementType, int? userId = null, string description = null)
        {
            if (quantity <= 0)
                throw new ArgumentException("Miktar sıfırdan büyük olmalıdır.");

            var product = _productRepository.GetById(productId);
            if (product == null)
                throw new ArgumentException("Ürün bulunamadı.");

            if (movementType == MovementType.StockOut && quantity > product.StockQuantity)
                throw new InvalidOperationException($"Yetersiz stok. Mevcut stok: {product.StockQuantity}");

            // Stok miktarını güncelle
            if (movementType == MovementType.StockIn)
                product.StockQuantity += quantity;
            else
                product.StockQuantity -= quantity;

            // Stok hareketi oluştur
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
            _stockMovementRepository.SaveChanges();
        }

        public int GetTotalStock()
        {
            return _productRepository.GetAll().Sum(p => p.StockQuantity);
        }

        public int GetTotalStockValue()
        {
            return (int)_productRepository.GetAll().Sum(p => p.StockQuantity * p.PurchasePrice);
        }

        public List<Product> GetLowStockProducts()
        {
            return _productRepository.Find(p => p.StockQuantity <= p.MinimumStockLevel).ToList();
        }
    }
} 