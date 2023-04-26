using CarRental.BlazorWasm.Models;
using CarRental.BlazorWasm.Models.Cars;
using CarRental.BlazorWasm.Models.Items;
using CarRental.BlazorWasm.Models.Register;
using System.Net;

namespace CarRental.BlazorWasm.Services
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
            BaseResponse carRes = await _apiService.GET<CarResponse>(EndPoint);

            CarResponse cars = carRes.GetResponse<CarResponse>();

            return cars.cars;
        }

        public async Task<string> DeleteItem(int id)
        {
            BaseResponse carRes = await _apiService.DELETE<MessageResponse>(EndPoint + '/' + id);

            MessageResponse message = carRes.GetResponse<MessageResponse>();

            return message.message;
        }
        public async Task<string> CreateItem(CarRequest car)
        {
            BaseResponse carRes = await _apiService.POST<CarRequest, MessageResponse>(EndPoint, car);

            MessageResponse message = carRes.GetResponse<MessageResponse>();

            return message.message;
        }

        public async Task<string> EditItem(int id, CarRequest car)
        {
            BaseResponse carRes = await _apiService.PUT<CarRequest, MessageResponse>(EndPoint + '/' + id, car);

            MessageResponse message = carRes.GetResponse<MessageResponse>();

            return message.message;
        }

        public CarRequest GetDefaultRequest()
        {
            return new CarRequest();
        }
    }
}
