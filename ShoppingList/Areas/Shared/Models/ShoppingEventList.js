angular.module("ShoppingEventList", ["ShoppingItem", "ShoppingItemAJAX"])

.factory("ShoppingEventList", ["ShoppingItem", "ShoppingItemAJAX", function (ShoppingItem, ShoppingItemAJAX)
{
    var ShoppingEventList = function(eventId)
    {
        this.eventId = eventId;
    };

    ShoppingEventList.prototype = Object.create(Array.prototype);
    ShoppingEventList.prototype.constructor = ShoppingEventList;

    // add item
    ShoppingEventList.prototype.addNewItemCallback = function (listItem)
    {
        ShoppingEventList.prototype.push(listItem);
    };

    ShoppingEventList.prototype.addNewItem = function (name)
    {
        var listItem = new ShoppingItem(0, name, false, this.eventId);
        listItem.addUpdate(ShoppingEventList.prototype.addNewItemCallback);
    };

    // remove item
    var removeItemCallback = function (shoppingItem)
    {
        if (shoppingItem)
        {
            var indexOfItemToRemove = ShoppingEventList.prototype.indexOf(shoppingItem);
            if (indexOfItemToRemove > -1)
            {
                ShoppingEventList.prototype.splice(indexOfItemToRemove, 1);
            }
        }
    };

    ShoppingEventList.prototype.removeItem = function (shoppingItem)
    {
        shoppingItem.shoppingEventId = null;
        shoppingItem.addUpdate(removeItemCallback);
    };

    // get data from the server
    ShoppingEventList.prototype.getResultSetCallback = function (resultSet)
    {
        ShoppingEventList.prototype.length = 0;

        for (var index in resultSet)
        {
            if (resultSet.hasOwnProperty(index))
            {
                var resultSetItem = resultSet[index];
                ShoppingEventList.prototype.push(new ShoppingItem(resultSetItem.id, resultSetItem.name, resultSetItem.isPurchased, resultSetItem.shoppingEventId));
            }
        }
    }

    ShoppingEventList.prototype.getShoppingItemsFromServer = function (shoppingDateId)
    {
        new ShoppingItemAJAX().getShoppingItemsByEventId(shoppingDateId, this.getResultSetCallback);
    };

    return ShoppingEventList;

}]);