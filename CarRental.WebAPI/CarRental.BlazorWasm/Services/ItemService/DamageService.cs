using CarRental.BlazorWasm.Models;
using CarRental.BlazorWasm.Models.Damages;
using CarRental.BlazorWasm.Models.Items;
using CarRental.BlazorWasm.Models.Staffs;

namespace CarRental.BlazorWasm.Services.ItemService
{
    public class DamageService
    {
        private readonly ApiService _apiService;

        public string EndPoint = "/api/damages";


        public DamageService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<Damage>> GetDamages()
        {
            DamageResponse damages = await _apiService.GET<DamageResponse>(EndPoint);

            return damages.damages;
        }
       
        public async Task<string> CreateDamage(DamageRequest damage)
        {
            MessageResponse message = await _apiService.POST<DamageRequest, MessageResponse>(EndPoint, damage);
            return message.message;
        }

        public DamageRequest GetDefaultRequest()
        {
            return new DamageRequest();
        }
    }


}
