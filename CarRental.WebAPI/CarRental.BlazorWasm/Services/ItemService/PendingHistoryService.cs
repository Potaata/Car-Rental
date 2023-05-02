using CarRental.BlazorWasm.Models;
using CarRental.BlazorWasm.Models.Items;
using CarRental.BlazorWasm.Models.Enums;

namespace CarRental.BlazorWasm.Services.ItemService
{
    public class PendingHistoryService
    {
        private readonly ApiService _apiService;

        public string EndPoint = "/api/admin/renthistory";
        public PendingHistoryService(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<List<PendingHistory>> GetAll()
        {
            PendingHistoryResponse pendingHistories = await _apiService.GET<PendingHistoryResponse>(EndPoint);
            List<PendingHistory> pendingHistoryList = pendingHistories.rents.Where(x => x.Status == StatusEnums.Pending).ToList();
            return pendingHistoryList;
        }
        public async Task<string> ApproveRequest(int id)
        {
            MessageResponse message = await _apiService.POST<EmptyRequest, MessageResponse>("/api/admin/approve-request" + '/' + id, new EmptyRequest { });

            return message.message;
        }
        public async Task<string> DenyRequest(int id)
        {
            MessageResponse message = await _apiService.POST<EmptyRequest, MessageResponse>("/api/admin/deny-request" + '/' + id, new EmptyRequest { });

            return message.message;
        }
    }

}
