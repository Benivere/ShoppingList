using System;
using System.Collections.Generic;
using ShoppingList.Application.Common;

namespace ShoppingList.Application.DatabaseModel
{
    public partial class Item
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

        public bool AddUpdateItem(Item item)
        {
            if (item == null)
            {
                return false;
            }

            CopyProperties(item);
            Validate();

            var success = Id == 0 ? Add() : Update();

            return success;
        }

        public bool DeleteItem(int itemId)
        {
            return Delete(itemId);
        }

        protected void Validate()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var verifications = new List<Verify>();

            verifications.Add(new Verify(Id >= 0).OnFailureShowMessage("Id value is out of range (0 = Add, >0 = Update)."));
            verifications.Add(new Verify(Name != null && Name.Trim() != "").OnFailureShowMessage("Name cannot be empty."));

            Verify.These(verifications);
        }

        protected virtual bool Add()
        {
            return true;
        }

        protected virtual bool Update()
        {
            return true;
        }

        protected virtual bool Delete(int itemId)
        {
            return true;
        }
    }
}