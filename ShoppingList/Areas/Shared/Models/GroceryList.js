angular.module("Shopping", ["ShoppingItem", "ShoppingAJAX"])

.factory("Shopping", ["ShoppingItem", "ShoppingAJAX", function (ShoppingItem, ShoppingAJAX)
{
    var Shopping = function () { };

    Shopping.prototype = Object.create(Array.prototype);
    Shopping.prototype.constructor = Shopping;

    Shopping.prototype.addNewItem = function (name)
    {
        var listItem = new ShoppingItem(0, name, false);
        listItem.addUpdate();
        Shopping.prototype.push(listItem);
    };

    Shopping.prototype.removeItem = function (listItem)
    {
        var indexOfItemToRemove = Shopping.prototype.indexOf(listItem);
        if (indexOfItemToRemove > -1)
        {
            listItem.delete();
            Shopping.prototype.splice(indexOfItemToRemove, 1);
        }
    };

    Shopping.prototype.getResultSetCallback = function (resultSet)
    {
        Shopping.prototype.length = 0;

        for (var index in resultSet)
        {
            if (resultSet.hasOwnProperty(index))
            {
                var resultSetItem = resultSet[index];
                Shopping.prototype.push(new ShoppingItem(resultSetItem.id, resultSetItem.name, resultSetItem.isPurchased));
            }
        }
    }

    Shopping.prototype.getShoppingItemsFromServer = function (shoppingDateId)
    {
        new ShoppingAJAX().getShoppingItems(shoppingDateId, this.getResultSetCallback);
    };

    return Shopping;
}]);