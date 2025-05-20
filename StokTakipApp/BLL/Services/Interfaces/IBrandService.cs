using System;
using System.Collections.Generic;
using Entities;

namespace BLL.Services.Interfaces
{
    public interface IBrandService
    {
        List<Brand> GetAllBrands();
        Brand GetBrandById(int id);
        List<Brand> SearchBrands(string searchTerm);
        
        void AddBrand(Brand brand);
        void UpdateBrand(Brand brand);
        void DeleteBrand(int id);
        
        bool HasProducts(int brandId);
    }
} 