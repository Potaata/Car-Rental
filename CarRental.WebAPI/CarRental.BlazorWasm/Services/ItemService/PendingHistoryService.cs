using CarRental.BlazorWasm.Models;
using CarRental.BlazorWasm.Models.Items;

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
            List<PendingHistory> pendingHistoryList = pendingHistories.pendingHistories.Where(x => x.Status == "Pending").ToList();
            return pendingHistoryList;
        }
        public async Task<string> ApproveRequest(int id)
        {
            MessageResponse message = await _apiService.PUT<EmptyRequest, MessageResponse>(EndPoint + '/' + id, null);

            return message.message;
        }
        public async Task<string> DenyRequest(int id)
        {
            MessageResponse message = await _apiService.PUT<EmptyRequest, MessageResponse>(EndPoint + '/' + id, null);

            return message.message;
        }
    }

}
