using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        // Temel CRUD işlemleri
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        
        // Sayfalama
        IEnumerable<T> GetPaged(int page, int pageSize);
        
        // Kaydetme işlemi
        int SaveChanges();
    }
} 