namespace ShoppingList.Application.Api.Interfaces
{
    public interface IUpdate<TEntityType>
    {
        bool DeleteItem(int itemId, IStorage<TEntityType> iStorage);

        TEntityType AddUpdateItem(TEntityType tEntityType, IStorage<TEntityType> iStorage);
    }
}
