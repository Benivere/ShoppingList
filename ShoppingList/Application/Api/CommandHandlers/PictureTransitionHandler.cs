using ShoppingList.Application.Api.Sql;
using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Application.Api.CommandHandlers
{
    public class PictureTransitionHandler
    {
        public Picture AddUpdate(Picture picture)
        {
            var dbContext = new ShoppingListDbContext();

            var pictureSqlStorage = new PictureSqlStorage(dbContext);

            var pictureToUpdate = pictureSqlStorage.GetById(picture.Id);
            pictureToUpdate.AddUpdateItem(picture, pictureSqlStorage);
            return pictureToUpdate;
        }

        public bool Delete(int pictureId)
        {
            var dbContext = new ShoppingListDbContext();

            var pictureSqlStorage = new PictureSqlStorage(dbContext);

            return new Picture().DeleteItem(pictureId, pictureSqlStorage);
        }
    }
}