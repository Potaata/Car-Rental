namespace CarRental.BlazorWasm.Models.Items
{
    public class RentHistoryResponse : SuccessResponse
    {
        public List<RentHistory> rents { get; set; }
    }
}
