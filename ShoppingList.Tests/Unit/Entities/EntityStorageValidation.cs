using Rhino.Mocks;
using ShoppingList.Application.Api.Interfaces;

namespace ShoppingList.Tests.Unit.Entities
{
    public class EntityStorageValidation<TEntity> where TEntity : IEntity, IUpdate<TEntity>
    {
        protected TEntity ValidateAdd(TEntity entity)
        {
            // Arrange
            var mockIStorage = MockRepository.GenerateMock<IStorage<TEntity>>();
            mockIStorage.Expect(s => s.Add(entity))
                        .Return(entity);

            // Act
            var updatedShoppingItem = entity.AddUpdateItem(entity, mockIStorage);

            // Assert
            mockIStorage.VerifyAllExpectations();

            return updatedShoppingItem;
        }

        protected TEntity ValidateUpdate(TEntity entity)
        {
            // Arrange
            var mockIStorage = MockRepository.GenerateMock<IStorage<TEntity>>();
            mockIStorage.Expect(s => s.Update(entity))
                        .Return(entity);

            // Act
            var updatedShoppingItem = entity.AddUpdateItem(entity, mockIStorage);

            // Assert
            mockIStorage.VerifyAllExpectations();

            return updatedShoppingItem;
        }

        protected bool ValidateDelete(TEntity entity)
        {
            // Arrange
            var mockIStorage = MockRepository.GenerateMock<IStorage<TEntity>>();
            mockIStorage.Expect(s => s.Delete(entity.Id))
                        .Return(true);

            // Act
            var success = entity.DeleteItem(entity.Id, mockIStorage);

            // Assert
            mockIStorage.VerifyAllExpectations();

            return success;
        }
    }
}
