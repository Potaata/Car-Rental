using CarRental.BlazorWasm.Models;
using CarRental.BlazorWasm.Models.Cars;
using CarRental.BlazorWasm.Models.Register;
using System.Net;

namespace CarRental.BlazorWasm.Services
{
    public class CarService: IItemService<Car>
    {
        private readonly ApiService _apiService;

        public string EndPoint = "/api/cars";
        
        public CarService(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<List<Car>> GetItems()
        {
            RegisterRequest req = new RegisterRequest
            {
                Email = "asdf@asdf.com",
                Username = "asjdflksajf",
                RawPassword = "asdfsadfsdfsa123123@sfAA",
                PhoneNumber = "123123123"
            };
            BaseResponse carRes = await _apiService.POST<RegisterRequest, CarResponse>(EndPoint, req);

            CarResponse cars = carRes.GetResponse<CarResponse>();

            return cars.cars;
        }
    }
}
