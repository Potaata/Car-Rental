namespace CarRental.BlazorWasm.Models.Items
{
    public class CarRequest: TableItemRequest
    {
        public string Model { get; set; }
        public float Price { get; set; }
        public string NumberPlate { get; set; }
        public string Color { get; set; }

        public CarRequest GetDefault()
        {
            return new CarRequest();
        }
    }
}
