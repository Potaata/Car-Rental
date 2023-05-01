namespace CarRental.BlazorWasm.Models.Cars
{
    public class CarListWithFileResponse: SuccessResponse
    {
        public List<CarInsertRequest> cars{ get; set; }
    }
}
