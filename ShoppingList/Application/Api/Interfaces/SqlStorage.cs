using System;
using System.Data.Entity;
using System.Linq;

namespace ShoppingList.Application.Api.Interfaces
{
    public class SqlStorage<T> : IStorage<T> where T : class, IValidate, IEntity
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public SqlStorage(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public T Add(T item)
        {
            item.Validate();

            _dbSet.Add(item);
            _dbContext.Entry(item).State = EntityState.Added;
            var count = _dbContext.SaveChanges();
            var success = count > 0;

            return success ? item : null;
        }

        public T Update(T item)
        {
            item.Validate();

            var count = _dbContext.SaveChanges();
            var success = count > 0;

            return success ? item : null;
        }

        public bool Delete(int id)
        {
            var itemToDelete = _dbSet.FirstOrDefault(x => x.Id == id);
            if (itemToDelete == null)
            {
                throw new Exception("Cannot locate existing row to delete");
            }

            _dbContext.Entry(itemToDelete).State = EntityState.Deleted;
            var count = _dbContext.SaveChanges();
            var success = count > 0;

            return success;
        }
    }
}