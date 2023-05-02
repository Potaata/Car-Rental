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
       
        public async Task<string> CreateItem(DamageRequest staff)
        {
            throw new Exception("We don't need this here");
        }

        public DamageRequest GetDefaultRequest()
        {
            return new DamageRequest();
        }
    }


}
