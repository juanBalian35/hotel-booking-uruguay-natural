using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccessInterface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected DbContext Context { get; set; }

        public GenericRepository(DbContext context)
        {
            Context = context;
        }

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            Context.Set<T>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public bool Exists(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = Context.Set<T>();
            return query.Where(filter).Any();
        }

        public T GetFirst(Expression<Func<T, bool>> filter, string includeProperties = "")
        {
            IQueryable<T> query = Context.Set<T>();
            query = IncludeFromString(query, includeProperties);
            
            return query.FirstOrDefault(filter);
        }

        public ICollection<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = Context.Set<T>();
            query = FilterQuery(query, filter);
            query = IncludeFromString(query, includeProperties);
            return query.ToList();
        }
        
        public ICollection<T> GetAllWithPagination(Expression<Func<T, bool>> filter = null,
            string includeProperties = "", 
            int page = 1, 
            int numItemsPerPage = 10)
        {
            IQueryable<T> query = Context.Set<T>();
            query = FilterQuery(query, filter);
            query = IncludeFromString(query, includeProperties);
            query = query.Skip((page - 1) * numItemsPerPage).Take(numItemsPerPage);
            return query.ToList();
        }

        public T Get(object id)
        {
            return Context.Set<T>().Find(id);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        private static IQueryable<T> FilterQuery(IQueryable<T> query, Expression<Func<T, bool>> filter = null)
        {
            return filter != null ? query.Where(filter) : query;
        }

        private static IQueryable<T> IncludeFromString(IQueryable<T> query, string includeProperties = "")
        {
            var propertiesArray = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            return propertiesArray.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
