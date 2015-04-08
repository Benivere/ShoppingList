using ShoppingList.Application.Api.Sql;
using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Application.Api.CommandHandlers
{
    public class ItemTransitionHandler
    {
        public Item AddUpdate(Item item)
        {
            var dbContext = new ShoppingListDbContext();

            var itemSqlDatabase = new ItemSqlStorage(dbContext);

            var itemToUpdate = itemSqlDatabase.GetById(item.Id);
            itemToUpdate.AddUpdateItem(item, itemSqlDatabase);
            return itemToUpdate;
        }

        public bool Delete(int itemId)
        {
            var dbContext = new ShoppingListDbContext();

            var itemSqlDatabase = new ItemSqlStorage(dbContext);

            return new Item().DeleteItem(itemId, itemSqlDatabase);
        }
    }
}