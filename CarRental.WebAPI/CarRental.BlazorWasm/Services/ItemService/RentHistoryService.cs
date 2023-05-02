using CarRental.BlazorWasm.Models.Items;
using CarRental.BlazorWasm.Models;

namespace CarRental.BlazorWasm.Services.ItemService
{
    public class RentHistoryService
    {
        private readonly ApiService _apiService;

        public string EndPoint = "/api/admin/renthistory";
        public RentHistoryService(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<List<RentHistory>> GetAll()
        {
            RentHistoryResponse rentHistories = await _apiService.GET<RentHistoryResponse>(EndPoint);
            return rentHistories.rents;
        }
        public async Task<List<RentHistory>> GetUserRentHistory()
        {
            RentHistoryResponse rentHistories = await _apiService.GET<RentHistoryResponse>(EndPoint);
            return rentHistories.rents;
        }

        public async Task<string> MarkCarTaken(int id)
        {
            MessageResponse message = await _apiService.POST<EmptyRequest, MessageResponse>("/api/admin/car-taken" + '/' + id, new EmptyRequest { });

            return message.message;
        }

        public async Task<string> ReturnCar(int id)
        {
            MessageResponse message = await _apiService.POST<EmptyRequest, MessageResponse>("/api/admin/car-returned" + '/' + id, new EmptyRequest { });

            return message.message;
        }
    }
}
