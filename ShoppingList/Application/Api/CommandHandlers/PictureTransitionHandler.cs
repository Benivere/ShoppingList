using ShoppingList.Application.Api.Sql;
using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Application.Api.CommandHandlers
{
    public class PictureTransitionHandler
    {
        public Picture AddUpdate(Picture picture)
        {
            var pictureToUpdate = new PictureSqlDatabase(picture);
            var success = pictureToUpdate.AddUpdateItem(picture);
            return success ? pictureToUpdate : null;
        }

        public bool Delete(int pictureId)
        {
            return new Picture().DeleteItem(pictureId);
        }
    }
}