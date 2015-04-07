angular.module("FileUpload", ["PictureAJAX"])

.factory("FileUpload", ["PictureAJAX", "$upload", function (PictureAJAX, $upload)
{
    /* jshint validthis:true */
    var FileUpload = this;
    FileUpload.title = "FileUpload";

    FileUpload.onFileSelect = function ($files, user)
    {
        //$files: an array of files selected, each file has name, size, and type.
        for (var i = 0; i < $files.length; i++)
        {
            var file = $files[i];
        }
    };

    return FileUpload;
}]);