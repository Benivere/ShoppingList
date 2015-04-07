angular.module("PictureAJAX", [])

.factory("PictureAJAX", ["$http", "$window", function ($http, $window)
{
    var PictureAJAX = function () { }

    PictureAJAX.prototype.constructor = PictureAJAX;

    PictureAJAX.prototype.getById = function (pictureId, resultSetCallback)
    {
        if (!pictureId)
        {
            return;
        }
        $http.get("/api/PictureApi/GetById/", { params: { id: pictureId } })
            .success(function (resultSet)
            {
                resultSetCallback(resultSet);
            });
    };

    PictureAJAX.prototype.addUpdate = function (picture)
    {
        var fd = new FormData();
        fd.append("file", picture.selectedFile);
        $http.put("/api/PictureApi/AddUpdate/", fd, {
            transformRequest: angular.identity,
            headers: { "Content-Type": undefined }
        })
        .success(function (resultSet)
        {
            picture.addUpdateCallBack(resultSet);
        })
        .error(function (exception, resultCode)
        {
            $window.alert("Update failed with error: " + resultCode);
        });
        //$upload.upload(
        //    {
        //        url: "/api/PictureApi/AddUpdate/",
        //        data: { name: "anonymous" },
        //        file: file // or list of files ($files) for html5 only
        //    })
        //    .progress(function (evt)
        //    {
        //        //console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));
        //    })
        //    .success(function (resultSet)
        //    {
        //        resultSetCallback(resultSet);
        //    })
        //    .error(function (err)
        //    {
        //        alert("Error occured during upload");
        //    });
    };

    PictureAJAX.prototype.delete = function (listItemId, resultSetCallback)
    {
        if (!listItemId)
        {
            resultSetCallback(false);
        }

        $http.delete("/api/PictureApi/Delete/", { params: { id: listItemId } })
            .success(function (resultSet)
            {
                resultSetCallback(resultSet);
            });
    };

    return PictureAJAX;
}]);