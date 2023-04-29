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
            BaseResponse staffRes = await _apiService.GET<StaffResponse>(EndPoint);

            StaffResponse staffs = staffRes.GetResponse<StaffResponse>();

            return staffs.staffs;
        }

        public async Task<string> DeleteItem(int id)
        {
            BaseResponse staffRes = await _apiService.DELETE<MessageResponse>(EndPoint + '/' + id);

            MessageResponse message = staffRes.GetResponse<MessageResponse>();

            return message.message;
        }
        public async Task<string> CreateItem(StaffRequest staff)
        {
            BaseResponse staffRes = await _apiService.POST<StaffRequest, MessageResponse>(EndPoint, staff);

            MessageResponse message = staffRes.GetResponse<MessageResponse>();

            return message.message;
        }

        public async Task<string> EditItem(int id, StaffRequest staff)
        {
            BaseResponse staffRes = await _apiService.PUT<StaffRequest, MessageResponse>(EndPoint + '/' + id, staff);

            MessageResponse message = staffRes.GetResponse<MessageResponse>();

            return message.message;
        }

        public StaffRequest GetDefaultRequest()
        {
            return new StaffRequest();
        }
    }
}
