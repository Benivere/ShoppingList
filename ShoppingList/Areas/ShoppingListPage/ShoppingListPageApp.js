angular.module("ShoppingListPageApp", ["ShoppingList", "ShoppingListItem"])

.controller("ShoppingListPageController", ["$scope", "ShoppingList", function($scope, ShoppingList)
{
    // -- new task
    $scope.newItemName = "";

    $scope.newItemKeyPress = function(event)
    {
        if (event.keyCode === 13)
        {
            $scope.shoppingListAddItem();
        }
    }

    $scope.newitemButtonClick = function()
    {
        $scope.shoppingListAddItem();
    }

    // -- task list
    $scope.shoppingList = new ShoppingList();

    $scope.getShoppingListFromServer = function(shoppingDateId)
    {
        $scope.shoppingList.getShoppingListItemsFromServer(shoppingDateId);
    }

    $scope.shoppingListAddItem = function()
    {
        if ($scope.newItemName.trim().length > 0)
        {
            $scope.shoppingList.addNewItem($scope.newItemName.trim());
        }
        $scope.newItemName = undefined;
    };

    $scope.shoppingListRemoveItem = function(listItem)
    {
        $scope.shoppingList.removeItem(listItem);
    };

    // -- task list item
    $scope.shoppingListItemCheckboxClick = function(listItem)
    {
        listItem.addUpdate();
    }

    $scope.shoppingListItemDisabled = function(listItem)
    {
        return listItem ? listItem.isPurchased : false;
    }

    // load from server
    var getIdFromUrl = function()
    {
        var urlId = 0;
        var url = window.location.pathname;
        var urlParts = url.split("/");
        if (urlParts.length > 0)
        {
            var lastPartOfUrl = urlParts[urlParts.length - 1];
            if (!isNaN(lastPartOfUrl))
            {
                urlId = lastPartOfUrl;
            }
        }
        return urlId;
    }

    var id = getIdFromUrl();
    $scope.getShoppingListFromServer(id);

    // -- refresh timer callback
    $scope.refreshTimerCallback = function()
    {
        $scope.getShoppingListFromServer(id);
    }
}])

.directive("refreshtimer", ["$interval", function ($interval)
{
    return {
        restrict: "E",
        link: function (scope, element, attrs)
        {
            var interval = attrs.interval * 1000;

            var timeoutId = $interval(function ()
            {
                scope.refreshTimerCallback();
            },
            interval);

            element.on("destroy", function ()
            {
                $interval.cancel(timeoutId);
            });
        }
    }
}]);
