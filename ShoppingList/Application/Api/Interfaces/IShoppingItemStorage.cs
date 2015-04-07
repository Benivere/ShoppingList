using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Application.Api.Interfaces
{
    public interface IShoppingItemStorage
    {
        bool Delete(int itemId);

        ShoppingItem Add(ShoppingItem shoppingItem);

        ShoppingItem Update(ShoppingItem shoppingItem);
    }
}
