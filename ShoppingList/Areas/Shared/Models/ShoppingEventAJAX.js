angular.module("ShoppingEventAJAX", [])

.factory("ShoppingEventAJAX", ["$http", function ($http)
{
    var ListItemAJAX = function () { }

    ListItemAJAX.prototype.constructor = ListItemAJAX;

    ListItemAJAX.prototype.getShoppingItemsByEventId = function (shoppingEventId, resultSetCallback)
    {
        shoppingEventId = shoppingEventId ? shoppingEventId : 0;
        $http.get("/api/ShoppingEvent/GetEventById/", { params: { id: shoppingEventId } })
            .success(function (resultSet)
            {
                resultSetCallback(resultSet);
            });
    };

    ListItemAJAX.prototype.addUpdateItem = function (listItem, resultSetCallback)
    {
        $http.put("/api/ShoppingEvent/AddUpdateItem/", listItem)
            .success(function (resultSet)
            {
                resultSetCallback(listItem, resultSet);
            });
    };

    ListItemAJAX.prototype.deleteItem = function (listItemId, resultSetCallback)
    {
        if (!listItemId)
        {
            resultSetCallback(false);
        }

        $http.delete("/api/ShoppingEvent/DeleteItem/", { params: { id: listItemId } })
            .success(function (resultSet)
            {
                resultSetCallback(resultSet);
            });
    };

    return ListItemAJAX;
}]);