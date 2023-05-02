using CarRental.BlazorWasm.Models.Enums;

namespace CarRental.BlazorWasm.Models.Items
{
    public class RentHistory
    {
        public int Id { get; set; }

        public string CarModel { get; set; }

        public float RentPrice { get; set; }
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public StatusEnums Status { get; set; }
    }
}
