using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ShoppingList.Application.Common;
using ShoppingList.Application.Extensions;
using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Application.Entities
{
    public class ItemEntity : Item
    {
        public ItemEntity()
        {
            Id = 0;
        }

        public ItemEntity(int id)
        {
            using (var dbContext = new ShoppingListDbContext())
            {
                var storedVersionOfItem = dbContext.Item.FirstOrDefault(x => x.Id == id);
                new Verify(storedVersionOfItem != null).OnFailureThrow(string.Format("Could not locate an Item with Id={0}.", id)).Immediately();
                CopyProperties(storedVersionOfItem);
            }
        }

        public ItemEntity(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            Id = item.Id;
            CopyProperties(item);
        }

        private void CopyProperties(Item item)
        {
            Name = item.Name;
            PictureId = item.PictureId;

            Validate();
        }

        public Item AddUpdateItem(Item item)
        {
            var returnItem = item.Id == 0 ? AddItem(item) : UpdateItem(item);

            return returnItem;
        }

        public bool DeleteItem(int itemId)
        {
            bool success;

            using (var dbContext = new ShoppingListDbContext())
            {
                var itemToDelete = dbContext.ShoppingItem.FirstOrDefault(x => x.Id == itemId);
                if (itemToDelete == null)
                {
                    throw new Exception("Cannot locate existing row to delete");
                }

                dbContext.Entry(itemToDelete).State = EntityState.Deleted;
                var count = dbContext.SaveChanges();
                success = count > 0;
            }

            return success;
        }

        private static Item AddItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            var entity = new ItemEntity(item);

            using (var dbContext = new ShoppingListDbContext())
            {
                var itemToAdd = entity.ToItem();
                dbContext.Item.Add(itemToAdd);
                dbContext.SaveChanges();

                return itemToAdd;
            }
        }

        private Item UpdateItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            CopyProperties(item);

            using (var dbContext = new ShoppingListDbContext())
            {
                var itemToUpdate = this.ToItem();
                dbContext.Entry(itemToUpdate).State = EntityState.Modified;
                dbContext.SaveChanges();

                return itemToUpdate;
            }
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