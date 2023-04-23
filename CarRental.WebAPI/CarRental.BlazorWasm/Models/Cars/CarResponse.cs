namespace CarRental.BlazorWasm.Models.Cars
{
    public class CarResponse: SuccessResponse
    {
        public List<Car> cars { get; set; }
    }
}
