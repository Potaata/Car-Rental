namespace CarRental.BlazorWasm.Models.Items
{
    public class PendingHistoryResponse: SuccessResponse
    {
        public List<PendingHistory> rents { get; set; }

    }
}
