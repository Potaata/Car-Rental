namespace CarRental.BlazorWasm.Models.Items
{
    public class RentResponse: SuccessResponse
    {
        public List<Rent> rents { get; set; }
    }
}
