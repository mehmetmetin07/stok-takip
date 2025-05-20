using System;
using System.Collections.Generic;
using Entities;

namespace BLL.Services.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
        Category GetCategoryById(int id);
        List<Category> SearchCategories(string searchTerm);
        
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);
        
        bool HasProducts(int categoryId);
    }
} 