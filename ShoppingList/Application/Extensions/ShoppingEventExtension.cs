using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Application.Extensions
{
    public static class ShoppingEventExtension
    {
        public static ShoppingEvent ToShoppingEvent(this ShoppingEvent item)
        {
            return new ShoppingEvent
            {
                Id = item.Id,
                ShoppingDate = item.ShoppingDate,
                DoneShopping = item.DoneShopping
            };
        }
    }
}