using ShoppingList.Application.Api.Sql;
using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Application.Api.CommandHandlers
{
    public class ItemTransitionHandler
    {
        public Item AddUpdate(Item item)
        {
            var itemToUpdate = new ItemSqlDatabase(item);
            var success = itemToUpdate.AddUpdateItem(item);
            return success ? itemToUpdate : null;
        }

        public bool Delete(int itemId)
        {
            return new Item().DeleteItem(itemId);
        }
    }
}