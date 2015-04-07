angular.module("Picture", ["PictureAJAX"])

.factory("Picture", ["PictureAJAX", function (PictureAJAX)
{
    var Picture = function (id, selectedFile)
    {
        this.id = id;
        this.selectedFile = selectedFile;
    };

    // add or update item
    Picture.prototype.shoppingItem = function () { }

    Picture.prototype.addUpdateCallBack = function(resultSetItem)
    {
        if (resultSetItem)
        {
            this.id = resultSetItem.id;
            resultSetItem = this;
        }

        if (this.shoppingItem)
        {
            this.shoppingItem.addPictureCallBack(resultSetItem);
        }
    }

    Picture.prototype.addUpdate = function(shoppingItem)
    {
        if (shoppingItem)
        {
            this.shoppingItem = shoppingItem;
        }
        new PictureAJAX().addUpdate(this);
    }

    // delete item
    Picture.prototype.deleteCallerCallback = function () { }

    Picture.prototype.deleteCallBack = function (isDeleted)
    {
        if (isDeleted && this.deleteCallerCallback)
        {
            this.deleteCallerCallback(this);
        }
    }

    Picture.prototype.delete = function (callerCallback)
    {
        if (callerCallback)
        {
            this.deleteCallerCallback = callerCallback;
        }
        new PictureAJAX().deleteItem(this);
    }

    Picture.prototype.getById = function (callerCallback)
    {
        new PictureAJAX().getById(this.id, callerCallback);
    }

    return Picture;

}]);