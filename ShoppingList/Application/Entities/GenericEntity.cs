using System;
using System.Data.Entity;
using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Application.Entities
{
    public class GenericEntity<TDataBaseModelTable>
        where TDataBaseModelTable : class
    {
        public GenericEntity()
        {

        }

        public GenericEntity(TDataBaseModelTable dataBaseModelTable)
        {
            if (dataBaseModelTable == null)
            {
                throw new ArgumentNullException("dataBaseModelTable");
            }
        }

        public int Id { get; set; }

        public static bool DeleteItem(TDataBaseModelTable dataBaseModelTable)
        {
            bool success;

            using (var dbContext = new ShoppingListDbContext())
            {
                if (dataBaseModelTable == null)
                {
                    throw new Exception("Cannot locate existing row to delete");
                }

                dbContext.Entry(dataBaseModelTable).State = EntityState.Deleted;
                var count = dbContext.SaveChanges();
                success = count > 0;
            }

            return success;
        }

        protected static TDataBaseModelTable AddItem(TDataBaseModelTable dataBaseModelTable)
        {
            if (dataBaseModelTable == null)
            {
                throw new ArgumentNullException("dataBaseModelTable");
            }

            using (var dbContext = new ShoppingListDbContext())
            {
                dbContext.Entry(dataBaseModelTable).State = EntityState.Added;
                dbContext.SaveChanges();

                return dataBaseModelTable;
            }
        }

        protected static TDataBaseModelTable UpdateItem(TDataBaseModelTable dataBaseModelTable)
        {
            if (dataBaseModelTable == null)
            {
                throw new ArgumentNullException("dataBaseModelTable");
            }

            using (var dbContext = new ShoppingListDbContext())
            {
                dbContext.Entry(dataBaseModelTable).State = EntityState.Modified;
                dbContext.SaveChanges();

                return dataBaseModelTable;
            }
        }
    }

    public interface IId
    {
        int Id { get; set; }
    }

    public interface IValidate
    {
        void Validate();
    }
}