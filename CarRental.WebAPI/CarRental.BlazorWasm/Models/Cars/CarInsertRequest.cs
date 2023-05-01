using CarRental.BlazorWasm.Models.Items;

namespace CarRental.BlazorWasm.Models.Cars
{
    public class CarInsertRequest: CarRequest
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
    }
}
