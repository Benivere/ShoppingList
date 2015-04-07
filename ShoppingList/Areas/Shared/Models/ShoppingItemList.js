angular.module("ShoppingItemList", ["ShoppingItem", "ShoppingItemAJAX"])

.factory("ShoppingItemList", ["ShoppingItem", "ShoppingItemAJAX", function (ShoppingItem, ShoppingItemAJAX)
{
    var ShoppingItemList = function (eventId)
    {
        this.eventId = eventId;
        this.eventId = this.eventId > 0 ? this.eventId : null;
    };

    ShoppingItemList.prototype = Object.create(Array.prototype);
    ShoppingItemList.prototype.constructor = ShoppingItemList;

    // --------- add item using name only

    ShoppingItemList.prototype.addUpdateCallback = function (shoppingItem)
    {
        if (shoppingItem)
        {
            this.push(shoppingItem);
        }
    };

    ShoppingItemList.prototype.addNewItem = function (name)
    {
        var listItem = new ShoppingItem(0, name, false, this.eventId);
        listItem.addUpdate(this);
    };

    // --------- remove item

    ShoppingItemList.prototype.deleteCallback = function (shoppingItem)
    {
        if (shoppingItem)
        {
            var indexOfItemToRemove = this.indexOf(shoppingItem);
            if (indexOfItemToRemove > -1)
            {
                this.splice(indexOfItemToRemove, 1);
            }
        }
    };

    ShoppingItemList.prototype.removeItem = function (shoppingItem)
    {
        if (shoppingItem)
        {
            shoppingItem.delete();
        }
    };

    // --------- get data from the server

    ShoppingItemList.prototype.getResultSetCallback = function (resultSet)
    {
        this.length = 0;

        for (var index in resultSet)
        {

            if (resultSet.hasOwnProperty(index))
            {
                var resultSetItem = resultSet[index];
                this.push(new ShoppingItem(resultSetItem.id, resultSetItem.name, resultSetItem.isPurchased, resultSetItem.shoppingEventId, resultSetItem.pictureId));
                if (!this.eventId && index < 1)
                {
                    this.eventId = resultSetItem.shoppingEventId;
                }
            }
        }
    }

    ShoppingItemList.prototype.getShoppingItemsFromServer = function ()
    {
        new ShoppingItemAJAX().getShoppingItemsByEventId(this);
    };

    return ShoppingItemList;

}]);