using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Application.Extensions
{
    public static class PictureExtension
    {
        public static Picture ToPicture(this Picture picture)
        {
            return new Picture(picture);
        }
    }
}