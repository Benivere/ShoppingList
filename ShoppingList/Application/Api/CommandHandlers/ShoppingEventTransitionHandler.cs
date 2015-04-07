using ShoppingList.Application.Api.Sql;
using ShoppingList.Application.DatabaseModel;
using ShoppingList.Application.Entities;

namespace ShoppingList.Application.Api.CommandHandlers
{
    public class ShoppingEventTransitionHandler
    {
        public ShoppingEvent AddUpdate(ShoppingEvent shoppingEvent)
        {
            var shoppingEventToUpdate = new ShoppingEventSqlDatabase(shoppingEvent);
            var success = shoppingEventToUpdate.AddUpdateItem(shoppingEvent);
            return success ? shoppingEventToUpdate : null;
        }

        public bool Delete(int shoppingEventId)
        {
            return new ShoppingEventEntity().DeleteItem(shoppingEventId);
        }
    }
}