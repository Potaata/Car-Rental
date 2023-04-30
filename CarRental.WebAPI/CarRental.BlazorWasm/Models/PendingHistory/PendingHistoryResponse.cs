namespace CarRental.BlazorWasm.Models.Items
{
    public class PendingHistoryResponse: SuccessResponse
    {
        public List<PendingHistory> pendingHistories { get; set; }

    }
}
