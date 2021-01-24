using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccessInterface
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
        bool Exists(Expression<Func<T, bool>> filter);

        T GetFirst(Expression<Func<T, bool>> filter, string includeProperties = "");
        ICollection<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        
        ICollection<T> GetAllWithPagination(Expression<Func<T, bool>> filter = null, 
            string includeProperties = "",
            int page = 1,
            int resultsPerPge = 10);
        
        T Get(object id);

        void Save();
    }
}
