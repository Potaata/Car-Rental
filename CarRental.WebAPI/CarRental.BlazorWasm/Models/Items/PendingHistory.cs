namespace CarRental.BlazorWasm.Models.Items
{
    public class PendingHistory
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string? ApprovedBy { get; set; }
        public string CarModel { get; set; }
        public string CarNumberPlate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Status;
        public int Price { get; set; }
    }
}
