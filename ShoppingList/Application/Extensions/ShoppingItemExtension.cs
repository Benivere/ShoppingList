using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Application.Extensions
{
    public static class ShoppingItemExtension
    {
        public static ShoppingItem ToShoppingItem(this ShoppingItem shoppingItem)
        {
            return new ShoppingItem
            {
                Id = shoppingItem.Id,
                ItemId = shoppingItem.ItemId,
                ShoppingEventId = shoppingItem.ShoppingEventId,
                Quantity = shoppingItem.Quantity,
                IsPurchased = shoppingItem.IsPurchased
            };
        }
    }
}