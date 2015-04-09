using ShoppingList.Application.Api.Sql;
using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Application.Api.CommandHandlers
{
    public class ShoppingEventTransitionHandler
    {
        public ShoppingEvent AddUpdate(ShoppingEvent shoppingEvent)
        {
            var dbContext = new ShoppingListDbContext();

            var shoppingEventSqlStorage = new ShoppingEventSqlStorage(dbContext);

            var itemToUpdate = shoppingEventSqlStorage.GetById(shoppingEvent.Id);
            itemToUpdate.AddUpdateItem(shoppingEvent, shoppingEventSqlStorage);
            return itemToUpdate;
        }

        public bool Delete(int shoppingEventId)
        {
            var dbContext = new ShoppingListDbContext();

            var shoppingEventSqlStorage = new ShoppingEventSqlStorage(dbContext);

            return new ShoppingEvent().DeleteItem(shoppingEventId, shoppingEventSqlStorage);
        }
    }
}