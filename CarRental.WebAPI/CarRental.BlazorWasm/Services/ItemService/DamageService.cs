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

        public async Task<List<DamageTableItem>> GetDamages()
        {
            DamageTableListResponse damages = await _apiService.GET<DamageTableListResponse>("/api/admin/damage-requests");

            return damages.damageRequests;
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

        public async Task<string> MarkPaid(int id)
        {
            MessageResponse resp = await _apiService.POST<EmptyRequest, MessageResponse>("/api/admin/mark-paid/" + id, new EmptyRequest { });
            return resp.message;
        }

        public async Task<string> SetDamageCost(int id, float cost)
        {
            MessageResponse resp = await _apiService.POST<DamageQuoteRequest, MessageResponse>("/api/admin/quote-price/" + id, new DamageQuoteRequest { Cost = cost });
            return resp.message;
        }
    }
}
