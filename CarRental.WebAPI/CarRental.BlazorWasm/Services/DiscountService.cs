using CarRental.BlazorWasm.Models.DiscountOffers;
using CarRental.BlazorWasm.Models.Users;


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
            DiscountOfferResponse dor = await _apiService.GET<DiscountOfferResponse>("/api/users/discount");

            if(dor.offer == null)
            {
                dor.offer = new DiscountOffer
                {
                    DiscountPercent = 0
                };
            }

            bool isRegular = (await _apiService.GET<IsRegularUserResponse>("/api/users/isRegular")).isRegular;

            if (isRegular)
            {
                dor.offer.DiscountPercent += 10; //10% for regular
            }

            bool isStaffOrAdmin = (await _apiService.GET<UserRoleResponse>("/api/users/role")).Role != "User";

            if (isStaffOrAdmin)
            {
                dor.offer.DiscountPercent += 25; //25% for staff or admin
            }

            return dor.offer;
        }
    }
}
