using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Services.Interfaces;
using DAL.Repositories;
using Entities;

namespace BLL.Services
{
    public class BrandService : IBrandService
    {
        private readonly IRepository<Brand> _brandRepository;
        private readonly IRepository<Product> _productRepository;

        public BrandService(IRepository<Brand> brandRepository, IRepository<Product> productRepository)
        {
            _brandRepository = brandRepository;
            _productRepository = productRepository;
        }

        public List<Brand> GetAllBrands()
        {
            return _brandRepository.GetAll().ToList();
        }

        public Brand GetBrandById(int id)
        {
            return _brandRepository.GetById(id);
        }

        public List<Brand> SearchBrands(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<Brand>();

            searchTerm = searchTerm.ToLower();
            return _brandRepository.Find(b => 
                b.Name.ToLower().Contains(searchTerm) || 
                (b.Description != null && b.Description.ToLower().Contains(searchTerm))
            ).ToList();
        }

        public void AddBrand(Brand brand)
        {
            _brandRepository.Add(brand);
            _brandRepository.SaveChanges();
        }

        public void UpdateBrand(Brand brand)
        {
            _brandRepository.Update(brand);
            _brandRepository.SaveChanges();
        }

        public void DeleteBrand(int id)
        {
            var brand = _brandRepository.GetById(id);
            if (brand != null)
            {
                if (HasProducts(id))
                    throw new Exception("Bu markaya ait ürünler var. Önce ürünleri başka markaya taşıyın veya silin.");
                
                _brandRepository.Remove(brand);
                _brandRepository.SaveChanges();
            }
        }

        public bool HasProducts(int brandId)
        {
            return _productRepository.Find(p => p.BrandId == brandId).Any();
        }
    }
} 