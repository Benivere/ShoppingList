namespace ShoppingList.Application.Api.Interfaces
{
    public interface IStorage<TEntityType>
    {
        bool Delete(int itemId);

        TEntityType Add(TEntityType tEntityType);

        TEntityType Update(TEntityType tEntityType);
    }
}
