using CarRental.BlazorWasm.Models;
using CarRental.BlazorWasm.Models.Cars;
using CarRental.BlazorWasm.Models.Items;

namespace CarRental.BlazorWasm.Services.ItemService
{
    public class CarService : IItemService<Car, CarRequest>
    {
        private readonly ApiService _apiService;

        public string EndPoint = "/api/cars";

        public CarService(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<List<Car>> GetItems()
        {
            CarResponse cars = await _apiService.GET<CarResponse>(EndPoint);

            return cars.cars;
        }

        public async Task<string> DeleteItem(int id)
        {
            MessageResponse message = await _apiService.DELETE<MessageResponse>(EndPoint + '/' + id);

            return message.message;
        }           
        public async Task<string> CreateItem(CarRequest car)
        {
            throw new Exception("shouldn't have been called");
        }

        public async Task<string> InsertItem(CarInsertRequest car)
        {
            MessageResponse messageResponse = await _apiService.POST<CarInsertRequest, MessageResponse>(EndPoint, car);
            return messageResponse.message;
        }

        public async Task<string> EditItem(int id, CarRequest car)
        {
            MessageResponse message = await _apiService.PUT<CarRequest, MessageResponse>(EndPoint + '/' + id, car);

            return message.message;
        }

        public CarRequest GetDefaultRequest()
        {
            return new CarRequest();
        }
    }
}
