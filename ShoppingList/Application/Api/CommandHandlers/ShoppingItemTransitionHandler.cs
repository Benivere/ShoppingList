using ShoppingList.Application.Api.Sql;
using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Application.Api.CommandHandlers
{
    public class ShoppingItemTransitionHandler
    {
        public ShoppingItem AddUpdate(ShoppingItem shoppingItem)
        {
            var shoppingItemSqlDatabase = new ShoppingItemSqlDatabase();

            var itemToUpdate = shoppingItemSqlDatabase.GetById(shoppingItem.Id);
            itemToUpdate.AddUpdateItem(shoppingItem, shoppingItemSqlDatabase);
            return itemToUpdate;
        }

        public bool Delete(int shoppingItemId)
        {
            var shoppingItemSqlDatabase = new ShoppingItemSqlDatabase();

            return new ShoppingItem().DeleteItem(shoppingItemId, shoppingItemSqlDatabase);
        }
    }
}