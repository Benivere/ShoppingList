angular.module("ShoppingItemAJAX", [])

.factory("ShoppingItemAJAX", ["$http", function ($http)
{
    var ShoppingItemAJAX = function () { }

    ShoppingItemAJAX.prototype.constructor = ShoppingItemAJAX;

    ShoppingItemAJAX.prototype.getShoppingItemsByEventId = function (shoppingList)
    {
        var shoppingEventId = shoppingList.eventId ? shoppingList.eventId : 0;
        var config = { params: { id: shoppingEventId } };
        $http.get("/api/ShoppingItemApi/GetByEventId/", config)
            .success(function (resultSet)
            {
                shoppingList.getResultSetCallback(resultSet);
            });
    };

    ShoppingItemAJAX.prototype.addUpdateItem = function (shoppingItem)
    {
        $http.put("/api/ShoppingItemApi/AddUpdateItem/", shoppingItem)
            .success(function (resultSet)
            {
                shoppingItem.addUpdateCallBack(resultSet);
            })
            .error(function ()
            {
                shoppingItem.addUpdateCallBack(false);
            });
    };

    ShoppingItemAJAX.prototype.deleteItem = function (shoppingItem)
    {
        if (!shoppingItem)
        {
            return;
        }

        var config = { params: { id: shoppingItem.id } };
        $http.delete("/api/ShoppingItemApi/DeleteItem/", config)
            .success(function (resultSet)
            {
                shoppingItem.deleteCallBack(resultSet);
            })
            .error(function ()
            {
                shoppingItem.deleteCallBack(false);
            });
    };

    return ShoppingItemAJAX;
}]);