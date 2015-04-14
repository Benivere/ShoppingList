using System;
using System.Collections.Generic;
using ShoppingList.Application.Api.Interfaces;
using ShoppingList.Application.Common;

namespace ShoppingList.Application.DatabaseModel
{
    public partial class Item : IValidate, IEntity, IUpdate<Item>
    {
        public Item(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            Id = item.Id;
            CopyProperties(item);
            Validate();
        }

        protected void CopyProperties(Item item)
        {
            if (item == null)
            {
                return;
            }

            Name = item.Name;
            PictureId = item.PictureId;
        }

        public Item AddUpdateItem(Item item, IStorage<Item> storage)
        {
            if (storage == null)
            {
                return null;
            }

            CopyProperties(item);
            Validate();

            var updatedItem = Id == 0 ? storage.Add(this) : storage.Update(this);

            return updatedItem;
        }

        public bool DeleteItem(int itemId, IStorage<Item> storage)
        {
            return storage.Delete(itemId);
        }

        public void Validate()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var verifications = new List<Verify>();

            verifications.Add(new Verify(Id >= 0).OnFailureShowMessage("Id value is out of range (0 = Add, >0 = Update)."));
            verifications.Add(new Verify(Name != null && Name.Trim() != "").OnFailureShowMessage("Name cannot be empty."));

            Verify.These(verifications);
        }
    }
}