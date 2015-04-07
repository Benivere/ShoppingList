using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ShoppingList.Application.Common;

namespace ShoppingList.Application.DatabaseModel
{
    public partial class Picture
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

        public bool AddUpdateItem(Picture picture)
        {
            if (picture == null)
            {
                throw new ArgumentNullException("picture");
            }

            CopyProperties(picture);
            Validate();

            var success = Id == 0 ? Add() : Update();

            return success;
        }

        public bool DeleteItem(int pictureId)
        {
            return Delete(pictureId);
        }

        protected void Validate()
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

        protected virtual bool Add()
        {
            return true;
        }

        protected virtual bool Update()
        {
            return true;
        }

        protected virtual bool Delete(int pictureId)
        {
            return true;
        }
    }
}