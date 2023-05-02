using CarRental.BlazorWasm.Models;
using CarRental.BlazorWasm.Models.Staffs;
using CarRental.BlazorWasm.Models.Items;

namespace CarRental.BlazorWasm.Services.ItemService
{
    public class StaffService : IItemService<Staff, StaffRequest>
    {
        private readonly ApiService _apiService;

        public string EndPoint = "/api/staffs";

        public StaffService(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<List<Staff>> GetItems()
        {
            StaffResponse staffs = await _apiService.GET<StaffResponse>(EndPoint);

            return staffs.users;
        }

        public async Task<string> DeleteItem(int id)
        {
            MessageResponse message = await _apiService.DELETE<MessageResponse>(EndPoint + '/' + id);

            return message.message;
        }
        public async Task<string> CreateItem(StaffRequest staff)
        {
            MessageResponse message = await _apiService.POST<StaffRequest, MessageResponse>(EndPoint, staff);

            return message.message;
        }

        public async Task<string> EditItem(int id, StaffRequest staff)
        {
            MessageResponse message = await _apiService.PUT<StaffRequest, MessageResponse>(EndPoint + '/' + id, staff);

            return message.message;
        }

        public StaffRequest GetDefaultRequest()
        {
            return new StaffRequest();
        }

        public async Task RegisterStaff()
        {
            
        }
    }
}
