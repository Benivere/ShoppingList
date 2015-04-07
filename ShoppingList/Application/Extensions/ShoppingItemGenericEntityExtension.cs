using ShoppingList.Application.DatabaseModel;
using ShoppingList.Application.Entities;

namespace ShoppingList.Application.Extensions
{
    public static class ShoppingItemGenericEntityExtension
    {
        public static ShoppingItem ToShoppingItem(this GenericEntityShoppingItemExample item)
        {
            return new ShoppingItem
            {
                Id = item.Id,
                Name = item.Name,
                IsPurchased = item.IsPurchased,
                ShoppingEventId = item.ShoppingEventId
            };
        }
    }
}