using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingList.Application.Common;
using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Application.Entities
{
    public class ShoppingEventEntity : ShoppingEvent
    {
        public ShoppingEventEntity()
        {
        }

        public ShoppingEventEntity(ShoppingEvent shoppingEvent)
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

        public bool AddUpdateItem(ShoppingEvent item)
        {
            CopyProperties(item);
            Validate();

            var returnItem = Id == 0 ? Add() : Update();

            return returnItem;
        }

        public bool DeleteItem(int shoppingEventId)
        {
            return Delete(shoppingEventId);
        }

        public void Validate()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var verifications = new List<Verify>();

            verifications.Add(new Verify(Id >= 0).OnFailureShowMessage("Id value is out of range (0 = Add, >0 = Update)."));

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

        protected virtual bool Delete(int id)
        {
            return true;
        }
    }
}