using CarRental.BlazorWasm.Models.Items;

namespace CarRental.BlazorWasm.Services
{
    public interface IItemService<TItem, TItemRequest> where TItem : TableItem where TItemRequest : TableItemRequest
    {
        public Task<List<TItem>> GetItems();
        public Task<string> DeleteItem(int id);
        public Task<string> CreateItem(TItemRequest item);
        public Task<string> EditItem(int id, TItemRequest item);

        public TItemRequest GetDefaultRequest();
    }
}
