angular.module("ShoppingEventPageApp", ["ShoppingItemList", "ShoppingItem", "Picture"])

.controller("ShoppingEventPageController", ["$window", "$scope", "ShoppingItemList", "ShoppingItem", "Picture", function ($window, $scope, ShoppingItemList, ShoppingItem, Picture)
{
    // --------- capture eventId from server

    var getIdFromUrl = function ()
    {
        var urlId = null;
        var url = window.location.pathname;
        var urlParts = url.split("/");
        if (urlParts.length > 0)
        {
            var lastPartOfUrl = urlParts[urlParts.length - 1];
            lastPartOfUrl = lastPartOfUrl.split("#")[0];
            if (!isNaN(lastPartOfUrl))
            {
                urlId = lastPartOfUrl;
            }
        }

        return urlId;
    }

    var eventId = getIdFromUrl();

    // --------- task list

    $scope.shoppingItemList = new ShoppingItemList(eventId);

    $scope.getShoppingEventItems = function ()
    {
        $scope.shoppingItemList.getShoppingItemsFromServer();
    }

    var shoppingItemListAddItem = function ()
    {
        if ($scope.newItemName.trim().length > 0)
        {
            $scope.shoppingItemList.addNewItem($scope.newItemName.trim());
        }
        $scope.newItemName = undefined;
    };

    var shoppingItemListRemoveItem = function (eventItem)
    {
        $scope.shoppingItemList.removeItem(eventItem);
    };

    // --------- task list item

    $scope.shoppingItemCheckboxClick = function (eventItem)
    {
        eventItem.addUpdate();
    }

    //$scope.shoppingItemDeleteClicked = function (eventItem)
    //{
    //    shoppingItemListRemoveItem(eventItem);
    //}

    $scope.shoppingItemDisabled = function (listItem)
    {
        return listItem ? listItem.isPurchased : false;
    }

    // --------- task item picture

    $scope.selectFileShoppingItem = null;

    $scope.addPictureClick = function (shoppingItem)
    {
        $scope.selectFileShoppingItem = shoppingItem;
    }

    $scope.shoppingItemAddPicture = function (shoppingItem, selectedFile)
    {
        if (!selectedFile)
        {
            return;
        }

        var picture = new Picture(0, selectedFile);
        picture.addUpdate(shoppingItem);
    }

    //$scope.shoppingItemShowPictureCallBack = function(image)
    //{
    //    $window.open(image, "_blank");
    //}


    $scope.shoppingItemShowPicture = function (shoppingItem)
    {
        $window.open("/api/PictureApi/GetById/?id=" + shoppingItem.pictureId, "_blank");
    }

    var addOrShowPicture = function (shoppingItem)
    {
        var result = shoppingItem ? (shoppingItem.pictureId != null && shoppingItem.pictureId != undefined) : false;
        return result;
    }

    $scope.shoppingItemAddPictureVisible = function (shoppingItem)
    {
        var result = !addOrShowPicture(shoppingItem);
        return result;
    }

    $scope.shoppingItemShowPictureVisible = function (shoppingItem)
    {
        var result = addOrShowPicture(shoppingItem);
        return result;
    }

    // --------- new task

    $scope.newItemName = "";

    $scope.newItemKeyPress = function (event)
    {
        if (event.keyCode === 13)
        {
            shoppingItemListAddItem();
        }
    }

    $scope.newitemButtonClick = function ()
    {
        shoppingItemListAddItem();
    }

    // --------- refresh timer callback

    $scope.refreshTimerCallback = function ()
    {
        $scope.getShoppingEventItems();
    }

    // --------- load the items from the server for this event

    $scope.getShoppingEventItems();

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
}])

.directive("fileModel", ["$parse", function ($parse)
{
    return {
        restrict: "A",
        link: function (scope, element, attrs)
        {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind("change", function ()
            {
                scope.$apply(function (childScope)
                {
                    modelSetter(scope, element[0].files[0]);
                    scope.shoppingItemAddPicture(scope.$parent.selectFileShoppingItem, element[0].files[0]);
                    element[0].value = "";
                });
            });
        }
    };
}]);

