angular.module("ShoppingEventItem", ["ShoppingEventAJAX"])

.factory("ShoppingEventItem", ["ShoppingEventAJAX", function (ShoppingEvemtAJAX)
{
    var ShoppingEventItem = function (id, vendorId, vendorName, date)
    {
        this.id = id;
        this.vendorId = vendorId;
        this.vendorName = vendorName;
        this.date = date;
    };

    ShoppingEventItem.prototype.addUpdateCallBack = function(listItem, resultSetItem)
    {
        listItem.id = resultSetItem.id;
    }

    ShoppingEventItem.prototype.deleteCallBack = function(listItem, resultSetItem)
    {
    }

    ShoppingEventItem.prototype.addUpdate = function()
    {
        new ShoppingEvemtAJAX().addItem(this, ShoppingEventItem.prototype.addUpdateCallBack);
    }

    ShoppingEventItem.prototype.delete = function ()
    {
        new ShoppingEvemtAJAX().deleteItem(this.id, ShoppingEventItem.prototype.deleteCallBack);
    }

    return ShoppingEventItem;

}]);