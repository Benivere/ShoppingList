using FluentAssertions;
using NUnit.Framework;
using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Tests.Unit.Entities
{
    [TestFixture]
    public class ItemStorageValidation : EntityStorageValidation<Item>
    {
        [Test]
        public void ValidateAddIsCalledOnNewObject()
        {
            // Arrange
            var itemToAdd = new Item
            {
                Id = 0,
                Name = "Item",
                PictureId = 1
            };

            // Act
            var updatedItem = ValidateAdd(itemToAdd);

            // Assert
            updatedItem.PictureId.ShouldBeEquivalentTo(itemToAdd.PictureId);
        }

        [Test]
        public void ValidateUpdateIsCalledOnExistingObject()
        {
            // Arrange
            var itemToAdd = new Item
            {
                Id = 1,
                Name = "Item",
                PictureId = 1
            };

            // Act
            var updatedItem = ValidateUpdate(itemToAdd);

            // Assert
            updatedItem.PictureId.ShouldBeEquivalentTo(itemToAdd.PictureId);
        }

        [Test]
        public void ValidateDeleteIsCalledOnExistingObject()
        {
            // Arrange
            var itemToAdd = new Item
            {
                Id = 1,
                Name = "Item",
                PictureId = 1
            };

            // Act
            var success = ValidateDelete(itemToAdd);

            // Assert
            success.ShouldBeEquivalentTo(true);
        }
    }
}