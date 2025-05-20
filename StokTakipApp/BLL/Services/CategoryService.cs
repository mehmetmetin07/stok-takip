using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Services.Interfaces;
using DAL.Repositories;
using Entities;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Product> _productRepository;

        public CategoryService(IRepository<Category> categoryRepository, IRepository<Product> productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll().ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public List<Category> SearchCategories(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<Category>();

            searchTerm = searchTerm.ToLower();
            return _categoryRepository.Find(c => 
                c.Name.ToLower().Contains(searchTerm) || 
                (c.Description != null && c.Description.ToLower().Contains(searchTerm))
            ).ToList();
        }

        public void AddCategory(Category category)
        {
            _categoryRepository.Add(category);
            _categoryRepository.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepository.Update(category);
            _categoryRepository.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category != null)
            {
                if (HasProducts(id))
                    throw new Exception("Bu kategoriye ait ürünler var. Önce ürünleri başka kategoriye taşıyın veya silin.");
                
                _categoryRepository.Remove(category);
                _categoryRepository.SaveChanges();
            }
        }

        public bool HasProducts(int categoryId)
        {
            return _productRepository.Find(p => p.CategoryId == categoryId).Any();
        }
    }
} 