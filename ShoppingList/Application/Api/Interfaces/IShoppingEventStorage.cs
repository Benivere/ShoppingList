using ShoppingList.Application.DatabaseModel;
using ShoppingList.Application.Entities;

namespace ShoppingList.Application.Api.Interfaces
{
    public interface IShoppingEventStorage
    {
        bool Delete(int id);

        ShoppingEventEntity Add(ShoppingEventEntity shoppingEvent);

        ShoppingEventEntity Update(ShoppingEventEntity shoppingEvent);
    }
}
