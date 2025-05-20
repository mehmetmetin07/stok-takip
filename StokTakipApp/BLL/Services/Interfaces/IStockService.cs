using System;
using System.Collections.Generic;
using Entities;

namespace BLL.Services.Interfaces
{
    public interface IStockService
    {
        List<StockMovement> GetAllStockMovements();
        StockMovement GetStockMovementById(int id);
        List<StockMovement> GetStockMovementsByProduct(int productId);
        List<StockMovement> GetStockMovementsByDateRange(DateTime startDate, DateTime endDate);
        List<StockMovement> GetStockMovementsByUser(int userId);
        
        void AddStockMovement(StockMovement stockMovement);
        void UpdateProductStock(int productId, int quantity, MovementType movementType, int? userId = null, string description = null);
        
        int GetTotalStock();
        int GetTotalStockValue();
        List<Product> GetLowStockProducts();
    }
} 