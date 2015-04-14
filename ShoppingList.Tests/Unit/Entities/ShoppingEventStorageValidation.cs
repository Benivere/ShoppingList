using System;
using FluentAssertions;
using NUnit.Framework;
using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Tests.Unit.Entities
{
    [TestFixture]
    public class ShoppingEventStorageValidation : EntityStorageValidation<ShoppingEvent>
    {
        [Test]
        public void ValidateAddIsCalledOnNewObject()
        {
            // Arrange
            var shoppingEventToAdd = new ShoppingEvent
            {
                Id = 0,
                ShoppingDate = DateTime.Now,
                DoneShopping = false,
                VendorId = 1
            };

            // Act
            var updatedShoppingItem = ValidateAdd(shoppingEventToAdd);

            // Assert
            updatedShoppingItem.VendorId.ShouldBeEquivalentTo(shoppingEventToAdd.VendorId);
        }

        [Test]
        public void ValidateUpdateIsCalledOnExistingObject()
        {
            // Arrange
            var shoppingEventToAdd = new ShoppingEvent
            {
                Id = 1,
                ShoppingDate = DateTime.Now,
                DoneShopping = false,
                VendorId = 1
            };

            // Act
            var updatedShoppingItem = ValidateUpdate(shoppingEventToAdd);

            // Assert
            updatedShoppingItem.VendorId.ShouldBeEquivalentTo(shoppingEventToAdd.VendorId);
        }

        [Test]
        public void ValidateDeleteIsCalledOnExistingObject()
        {
            // Arrange
            var shoppingEventToAdd = new ShoppingEvent
            {
                Id = 1,
                ShoppingDate = DateTime.Now,
                DoneShopping = false,
                VendorId = 1
            };

            // Act
            var success = ValidateDelete(shoppingEventToAdd);

            // Assert
            success.ShouldBeEquivalentTo(true);
        }
    }
}
