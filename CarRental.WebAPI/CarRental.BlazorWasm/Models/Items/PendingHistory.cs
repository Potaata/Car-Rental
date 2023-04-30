namespace CarRental.BlazorWasm.Models.Items
{
    public class PendingHistory
    {
        public string CustomerName { get; set; }
        public string CarModel { get; set; }
        public DateOnly RentDate { get; set; } 
        public DateOnly ReturnDate  { get; set; }
        public float RentPrice { get; set; }
    }
}
