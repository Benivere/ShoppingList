using System;
using System.Collections.Generic;
using ShoppingList.Application.Common;
using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Application.Entities
{
    public class ItemValidation : Item
    {
        public ItemValidation()
        {

        }

        public ItemValidation(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            Id = item.Id;
            Name = item.Name;
            PictureId = item.PictureId;

            Validate();
        }

        protected void Validate()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var verifications = new List<Verify>();

            verifications.Add(new Verify(Id >= 0).OnFailureThrow("Id value is out of range (0 = Add, >0 = Update)."));
            verifications.Add(new Verify(Name != null && Name.Trim() != "").OnFailureThrow("Name cannot be empty."));

            Verify.These(verifications);
        }
    }
}