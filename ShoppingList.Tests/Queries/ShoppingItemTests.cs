using FluentAssertions;
using NUnit.Framework;
using ShoppingList.Application.Api.Sql;
using ShoppingList.Application.Entities;

namespace ShoppingList.Tests.Queries
{
    [TestFixture]
    public class ShoppingItemTests
    {
        [Test]
        public void GetAllItems()
        {
            var resultSet = new ShoppingItemSqlQueries().GetAllItems();

            resultSet.Count.Should().BeGreaterOrEqualTo(1);
        }

        [Test, Ignore("Update once we have new test data")]
        public void GetShoppingItemsWhenShoppingEventIdIsZero()
        {
            //const int shoppingEventId = 0;

            //var resultSet = new ShoppingItemSqlQueries().GetByEventId(shoppingEventId);

            //resultSet.Count.Should().BeGreaterOrEqualTo(1);
            //resultSet.Count(x => x.Item.Name == "Dummy Item 3").ShouldBeEquivalentTo(0);
            //resultSet.Count(x => x.Name == "asdf").ShouldBeEquivalentTo(1);
        }

        [Test, Ignore("Update once we have new test data")]
        public void GetShoppingItemsWhenShoppingEventIdIsNotZero()
        {
            //const int shoppingEventId = 1;

            //var resultSet = new ShoppingItemSqlQueries().GetByEventId(shoppingEventId);

            //resultSet.Count.Should().BeGreaterOrEqualTo(1);
            //resultSet.Count(x => x.Name == "Dummy Item 3").ShouldBeEquivalentTo(1);
            //resultSet.Count(x => x.Name == "asdf").ShouldBeEquivalentTo(0);
        }
    }
}
