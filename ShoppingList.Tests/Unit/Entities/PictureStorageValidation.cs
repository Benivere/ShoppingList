using FluentAssertions;
using NUnit.Framework;
using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Tests.Unit.Entities
{
    [TestFixture]
    public class PictureStorageValidation : EntityStorageValidation<Picture>
    {
        private byte[] _picture;

        [TestFixtureSetUp]
        protected void Setup()
        {
            _picture = new byte[4];
            _picture[0] = (byte)'ÿ';
            _picture[1] = (byte)'Ø';
            _picture[2] = (byte)'ÿ';
        }

        [Test]
        public void ValidateAddIsCalledOnNewObject()
        {
            // Arrange
            var pictureToAdd = new Picture
            {
                Id = 0,
                PictureContent = _picture
            };

            // Act
            var updatedPicture = ValidateAdd(pictureToAdd);

            // Assert
            updatedPicture.PictureContent.ShouldBeEquivalentTo(pictureToAdd.PictureContent);
        }

        [Test]
        public void ValidateUpdateIsCalledOnExistingObject()
        {
            // Arrange
            var pictureToAdd = new Picture
            {
                Id = 1,
                PictureContent = _picture
            };

            // Act
            var updatedPicture = ValidateUpdate(pictureToAdd);

            // Assert
            updatedPicture.PictureContent.ShouldBeEquivalentTo(pictureToAdd.PictureContent);
        }

        [Test]
        public void ValidateDeleteIsCalledOnExistingObject()
        {
            // Arrange
            var pictureToAdd = new Picture
            {
                Id = 1,
                PictureContent = _picture
            };

            // Act
            var success = ValidateDelete(pictureToAdd);

            // Assert
            success.ShouldBeEquivalentTo(true);
        }
    }
}