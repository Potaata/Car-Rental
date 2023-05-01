using CarRental.BlazorWasm.Models;
using CarRental.BlazorWasm.Models.Rents;

namespace CarRental.BlazorWasm.Services
{
    public class RentService
    {
        private readonly ApiService _apiService;

        public RentService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<string> RentCar(int carId, DateTime from, DateTime to, float dailyPrice)
        {
            string apiEndpoint = "/api/user/add-rent";
            float totalPrice = (to - from).Days * dailyPrice;
            AddRentRequest addRent = new AddRentRequest
            {
                CarId = carId,
                FromDate = from,
                ToDate = to,
                Price = totalPrice
            };
            MessageResponse resp = await _apiService.POST<AddRentRequest, MessageResponse>(apiEndpoint, addRent);
            return resp.message;
        }
    }
}
