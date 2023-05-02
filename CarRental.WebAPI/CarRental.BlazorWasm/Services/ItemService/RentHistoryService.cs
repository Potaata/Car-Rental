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
            return rentHistories.rentHistories;
        }
        public async Task<List<RentHistory>> GetUserRentHistory()
        {
            RentHistoryResponse rentHistories = await _apiService.GET<RentHistoryResponse>(EndPoint);
            return rentHistories.rentHistories;
        }
    }
}
