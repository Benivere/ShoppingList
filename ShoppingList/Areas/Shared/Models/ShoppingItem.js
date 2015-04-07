angular.module("ShoppingItem", ["ShoppingItemAJAX"])

.factory("ShoppingItem", ["ShoppingItemAJAX", function (ShoppingItemAJAX)
{
    var ShoppingItem = function (id, name, isPurchased, shoppingEventId, pictureId)
    {
        this.id = id;
        this.name = name;
        this.isPurchased = isPurchased;
        this.shoppingEventId = shoppingEventId;
        this.pictureId = pictureId;
    };

    // add or update item
    ShoppingItem.prototype.addUpdateCaller = null;

    ShoppingItem.prototype.addPictureCallBack = function(picture)
    {
        this.pictureId = picture.id;
        new ShoppingItemAJAX().addUpdateItem(this);
    }

    ShoppingItem.prototype.addUpdateCallBack = function (resultSetItem)
    {
        if (resultSetItem)
        {
            this.id = resultSetItem.id;
            this.name = resultSetItem.name;
            this.isPurchased = resultSetItem.isPurchased;
            this.shoppingEventId = resultSetItem.shoppingEventId;
            this.pictureId = resultSetItem.pictureId;
        }

        if (resultSetItem && this.addUpdateCaller)
        {
            this.addUpdateCaller.addUpdateCallback(this);
            // unsubscribe the caller
            this.addUpdateCaller = null;
        }
    }

    ShoppingItem.prototype.addUpdate = function (addUpdateCaller)
    {
        // subscribe the caller
        if (addUpdateCaller)
        {
            this.addUpdateCaller = addUpdateCaller;
        }

        new ShoppingItemAJAX().addUpdateItem(this);
    }

    // delete item
    ShoppingItem.prototype.deleteCaller = null;

    ShoppingItem.prototype.deleteCallBack = function (isDeleted)
    {
        if (isDeleted && this.deleteCaller)
        {
            this.deleteCaller.deleteCallback(this);
            // unsubscribe the caller
            this.deleteCaller = null;
        }
    }

    ShoppingItem.prototype.delete = function (deleteCaller)
    {
        // subscribe the caller
        if (deleteCaller)
        {
            this.deleteCaller = deleteCaller;
        }

        new ShoppingItemAJAX().deleteItem(this);
    }

    return ShoppingItem;

}]);