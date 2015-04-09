using System;
using System.Collections.Generic;
using ShoppingList.Application.Api.Interfaces;
using ShoppingList.Application.Common;

namespace ShoppingList.Application.DatabaseModel
{
    public partial class ShoppingItem : IValidate, IEntity
    {
        public ShoppingItem()
        {
        }

        public ShoppingItem(ShoppingItem shoppingItem)
        {
            if (shoppingItem == null)
            {
                throw new ArgumentNullException("shoppingItem");
            }

            Id = shoppingItem.Id;
            CopyProperties(shoppingItem);
            Validate();
        }

        protected void CopyProperties(ShoppingItem shoppingItem)
        {
            if (shoppingItem == null)
            {
                throw new ArgumentNullException("shoppingItem");
            }

            ItemId = shoppingItem.ItemId;
            ShoppingEventId = shoppingItem.ShoppingEventId;
            Quantity = shoppingItem.Quantity;
            IsPurchased = shoppingItem.IsPurchased;
            Item = shoppingItem.Item;
            ShoppingEvent = shoppingItem.ShoppingEvent;
        }

        public ShoppingItem AddUpdateItem(ShoppingItem shoppingItem, IStorage<ShoppingItem> storage)
        {
            if (shoppingItem == null)
            {
                return null;
            }

            if (Id == 0)
            {
                CopyProperties(shoppingItem);
            }
            else
            {
                Quantity = shoppingItem.Quantity;
                IsPurchased = shoppingItem.IsPurchased;
            }

            Validate();

            var updatedItem = Id == 0 ? storage.Add(this) : storage.Update(this);

            return updatedItem;
        }

        public bool DeleteItem(int shoppingItemId, IStorage<ShoppingItem> storage)
        {
            return storage.Delete(shoppingItemId);
        }

        public void Validate()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var verifications = new List<Verify>();

            verifications.Add(new Verify(Id >= 0).OnFailureShowMessage("Id value is out of range (0 = Add, >0 = Update)."));

            if (Id > 0)
            {
                verifications.Add(new Verify(ItemId > 0).OnFailureShowMessage("ItemId is not a valid reference."));
                verifications.Add(new Verify(ShoppingEventId > 0).OnFailureShowMessage("ShoppingEventId is not a valid reference."));
            }

            Verify.These(verifications);
        }
    }
}