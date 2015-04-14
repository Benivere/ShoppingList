using System;
using FluentAssertions;
using NUnit.Framework;
using ShoppingList.Application.Common;
using ShoppingList.Application.DatabaseModel;

// ReSharper disable ObjectCreationAsStatement

namespace ShoppingList.Tests.Unit.Entities
{
    [TestFixture]
    public class ShoppingItemValidationTests
    {
        [Test]
        public void ValidatingShoppingItemShouldNotFailWithCorrectParameters()
        {
            var item = new ShoppingItem { Id = 0, ItemId = 2, ShoppingEventId = 3, Quantity = 0, IsPurchased = false };

            Action action = () => new ShoppingItem(item);

            action.ShouldNotThrow();
        }

        [Test]
        public void ValidatingShoppingItemShouldFailWithIncorrectParameters()
        {
            var item = new ShoppingItem { Id = 0, ItemId = 0, ShoppingEventId = 3, Quantity = 0, IsPurchased = false };

            Action action = () => new ShoppingItem(item);

            action.ShouldThrow<VerificationFailedException>().WithMessage("One or more validations have failed.");
        }
    }
}
