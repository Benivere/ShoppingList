using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingList.Application.Extensions;
using ShoppingList.Application.Common;
using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Application.Entities
{
    public class GenericEntityShoppingItemExample : GenericEntity<ShoppingItem>, IId, IValidate
    {
        public GenericEntityShoppingItemExample(ShoppingItem shoppingItem)
            : base(shoppingItem)
        {
            Id = shoppingItem.Id;
            Name = shoppingItem.Name;
            IsPurchased = shoppingItem.IsPurchased;
            ShoppingEventId = shoppingItem.ShoppingEventId;

            Validate();
        }

        public int? ShoppingEventId { get; set; }
        public bool? IsPurchased { get; set; }
        public string Name { get; set; }

        public static ShoppingItem AddUpdate(ShoppingItem shoppingItem)
        {
            ShoppingItem returnItem;
            var itemToUpdate = new GenericEntityShoppingItemExample(shoppingItem);

            if (itemToUpdate.Id == 0)
            {
                returnItem = AddItem(itemToUpdate.ToShoppingItem());
            }
            else
            {
                var storedVersionOfItem = new ShoppingListDbContext().ShoppingItem.FirstOrDefault(x => x.Id == shoppingItem.Id);
                if (storedVersionOfItem == null)
                {
                    throw new Exception("Cannot locate existing row to update");
                }

                storedVersionOfItem = UpdateValues(itemToUpdate, storedVersionOfItem);
                returnItem = UpdateItem(storedVersionOfItem);
            }

            return returnItem;
        }

        public bool Delete(int id)
        {
            var itemToDelete = new ShoppingListDbContext().ShoppingItem.FirstOrDefault(x => x.Id == id);
            if (itemToDelete == null)
            {
                throw new Exception("Cannot locate existing row to delete");
            }

            bool success = DeleteItem(itemToDelete);

            return success;
        }

        protected static ShoppingItem UpdateValues(GenericEntityShoppingItemExample fromItem, ShoppingItem toItem)
        {
            toItem.Name = fromItem.Name;
            toItem.IsPurchased = fromItem.IsPurchased;
            toItem.ShoppingEventId = fromItem.ShoppingEventId;

            return toItem;
        }

        public void Validate()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var verifications = new List<Verify>();

            verifications.Add(new Verify(Id >= 0).OnFailureThrow("Id value is out of range (0 = Add, >0 = Update)."));
            verifications.Add(new Verify(Name != null && Name.Trim() != "").OnFailureThrow("Name cannot be empty."));

            Verify.These(verifications);
        }
    }
}