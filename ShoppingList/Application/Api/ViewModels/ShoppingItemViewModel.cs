using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Application.Api.ViewModels
{
    public class ShoppingItemViewModel
    {
        public ShoppingItemViewModel()
        {
        }


        public ShoppingItemViewModel(ShoppingItem shoppingItem)
        {
            Id = shoppingItem.Id;
            Name = shoppingItem.Item.Name;
            IsPurchased = shoppingItem.IsPurchased;
            ShoppingEventId = shoppingItem.ShoppingEventId;
            PictureId = shoppingItem.Item.PictureId;
            Quantity = shoppingItem.Quantity;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPurchased { get; set; }
        public int ShoppingEventId { get; set; }
        public int? PictureId { get; set; }
        public int Quantity { get; set; }
    }
}