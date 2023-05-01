namespace CarRental.BlazorWasm.Models.Rents
{
    public class AddRentRequest
    {
        public int CarId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public float Price { get; set; }
    }
}
