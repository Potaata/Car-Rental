using CarRental.BlazorWasm.Models;
using CarRental.BlazorWasm.Models.Damages;
using CarRental.BlazorWasm.Models.Items;
using CarRental.BlazorWasm.Models.Staffs;

namespace CarRental.BlazorWasm.Services.ItemService
{
    public class DamageService : IItemService<Damage, DamageRequest>
    {
        private readonly ApiService _apiService;

        public string EndPoint = "/api/damages";


        public DamageService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<Damage>> GetItems()
        {
            throw new Exception("We don't need this here");
        }

        public async Task<string> DeleteItem(int id)
        {
            throw new Exception("We don't need this here");

        }
        public async Task<string> CreateItem(DamageRequest staff)
        {
            throw new Exception("We don't need this here");
        }

        public async Task<string> EditItem(int id, DamageRequest damage)
        {
            MessageResponse message = await _apiService.PUT<DamageRequest, MessageResponse>(EndPoint + '/' + id, damage);

            return message.message;
        }

        public DamageRequest GetDefaultRequest()
        {
            return new DamageRequest();
        }
    }


}
