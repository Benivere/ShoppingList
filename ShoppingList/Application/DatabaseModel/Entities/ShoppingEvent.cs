using System;
using System.Collections.Generic;
using ShoppingList.Application.Api.Interfaces;
using ShoppingList.Application.Common;

namespace ShoppingList.Application.DatabaseModel
{
    public partial class ShoppingEvent : IValidate, IEntity
    {
        public ShoppingEvent(ShoppingEvent shoppingEvent)
        {
            if (shoppingEvent == null)
            {
                throw new ArgumentNullException("shoppingEvent");
            }

            Id = shoppingEvent.Id;
            CopyProperties(shoppingEvent);
            Validate();
        }

        protected void CopyProperties(ShoppingEvent shoppingEvent)
        {
            if (shoppingEvent == null)
            {
                throw new ArgumentNullException("shoppingEvent");
            }

            ShoppingDate = shoppingEvent.ShoppingDate;
            DoneShopping = shoppingEvent.DoneShopping;
        }

        public ShoppingEvent AddUpdateItem(ShoppingEvent shoppingEvent, IStorage<ShoppingEvent> storage)
        {
            if (shoppingEvent == null)
            {
                return null;
            }

            CopyProperties(shoppingEvent);
            Validate();

            var updatedItem = Id == 0 ? storage.Add(this) : storage.Update(this);

            return updatedItem;
        }

        public bool DeleteItem(int shoppingEventId, IStorage<ShoppingEvent> storage)
        {
            return storage.Delete(shoppingEventId);
        }

        public void Validate()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var verifications = new List<Verify>();

            verifications.Add(new Verify(Id >= 0).OnFailureShowMessage("Id value is out of range (0 = Add, >0 = Update)."));

            Verify.These(verifications);
        }
    }
}