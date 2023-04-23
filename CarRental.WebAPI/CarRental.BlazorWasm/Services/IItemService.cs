namespace CarRental.BlazorWasm.Services
{
    public interface IItemService<TItem>
    {
        public Task<List<TItem>> GetItems();
    }
}
