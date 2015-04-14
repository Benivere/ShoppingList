using System;
using System.Collections.Generic;
using ShoppingList.Application.Api.Interfaces;
using ShoppingList.Application.Common;

namespace ShoppingList.Application.DatabaseModel
{
    public partial class Picture : IValidate, IEntity, IUpdate<Picture>
    {
        public Picture(Picture picture)
        {
            if (picture == null)
            {
                throw new ArgumentNullException("picture");
            }

            Id = picture.Id;
            CopyProperties(picture);
            Validate();
        }

        protected void CopyProperties(Picture picture)
        {
            if (picture == null)
            {
                return;
            }

            PictureContent = picture.PictureContent;
        }

        public Picture AddUpdateItem(Picture picture, IStorage<Picture> storage)
        {
            if (picture == null)
            {
                throw new ArgumentNullException("picture");
            }

            CopyProperties(picture);
            Validate();

            var updatedItem = Id == 0 ? storage.Add(this) : storage.Update(this);

            return updatedItem;
        }

        public bool DeleteItem(int pictureId, IStorage<Picture> storage)
        {
            return storage.Delete(pictureId);
        }

        public void Validate()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var verifications = new List<Verify>();

            verifications.Add(new Verify(Id >= 0).OnFailureShowMessage("Id value is out of range (0 = Add, >0 = Update)."));
            verifications.Add(new Verify(PictureContent != null && PictureContent.Length > 3 &&
                                         PictureContent[0] == 'ÿ' &&
                                         PictureContent[1] == 'Ø' &&
                                         PictureContent[2] == 'ÿ')
                                  .OnFailureShowMessage("The image does not appear to be a valid JPG file."));

            Verify.These(verifications);
        }
    }
}