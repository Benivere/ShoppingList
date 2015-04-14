using FluentAssertions;
using NUnit.Framework;
using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Tests.Unit.Entities
{
    [TestFixture]
    public class ShoppingItemStorageValidation : EntityStorageValidation<ShoppingItem>
    {
        [Test]
        public void ValidateAddIsCalledOnNewObject()
        {
            // Arrange
            var shoppingItemToAdd = new ShoppingItem
            {
                Id = 0,
                ItemId = 2,
                ShoppingEventId = 3,
                IsPurchased = false,
                Quantity = 5
            };

            // Act
            var updatedShoppingItem = ValidateAdd(shoppingItemToAdd);

            // Assert
            updatedShoppingItem.ItemId.ShouldBeEquivalentTo(shoppingItemToAdd.ItemId);
        }

        [Test]
        public void ValidateUpdateIsCalledOnExistingObject()
        {
            // Arrange
            var shoppingItemToAdd = new ShoppingItem
            {
                Id = 1,
                ItemId = 2,
                ShoppingEventId = 3,
                IsPurchased = false,
                Quantity = 5
            };

            // Act
            var updatedShoppingItem = ValidateUpdate(shoppingItemToAdd);

            // Assert
            updatedShoppingItem.ItemId.ShouldBeEquivalentTo(shoppingItemToAdd.ItemId);
        }

        [Test]
        public void ValidateDeleteIsCalledOnExistingObject()
        {
            // Arrange
            var shoppingItemToAdd = new ShoppingItem
            {
                Id = 1,
                ItemId = 2,
                ShoppingEventId = 3,
                IsPurchased = false,
                Quantity = 5
            };

            // Act
            var success = ValidateDelete(shoppingItemToAdd);

            // Assert
            success.ShouldBeEquivalentTo(true);
        }
    }
}
