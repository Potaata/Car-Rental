using CarRental.BlazorWasm.Models;
using CarRental.BlazorWasm.Models.Damages;
using CarRental.BlazorWasm.Models.Items;
using CarRental.BlazorWasm.Models.Staffs;

namespace CarRental.BlazorWasm.Services.ItemService
{
    public class DamageService
    {
        private readonly ApiService _apiService;


        public DamageService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<Damage>> GetDamages()
        {
            DamageResponse damages = await _apiService.GET<DamageResponse>("");

            return damages.damages;
        }

        public async Task<string> CreateDamage(string Description, int RentId)
        {
            CreateDamageRequest damage = new CreateDamageRequest
            {
                Description = Description,
                RentId = RentId
            };
            MessageResponse message = await _apiService.POST<CreateDamageRequest, MessageResponse>("/api/users/damage-request", damage);
            return message.message;
        }

    }
}
