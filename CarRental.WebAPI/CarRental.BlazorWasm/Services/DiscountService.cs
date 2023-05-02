using CarRental.BlazorWasm.Models.DiscountOffers;

namespace CarRental.BlazorWasm.Services
{
    public class DiscountService
    {

        private readonly ApiService _apiService;
        public DiscountService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<DiscountOffer> GetOffer()
        {
            DiscountOfferResponse dor = await _apiService.GET<DiscountOfferResponse>("/api/users/notifications");
            return dor.offer;
        }
    }
}
