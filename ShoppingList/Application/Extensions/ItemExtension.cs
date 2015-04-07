using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Application.Extensions
{
    public static class ItemExtension
    {
        public static Item ToItem(this Item item)
        {
            return new Item(item);
        }
    }
}