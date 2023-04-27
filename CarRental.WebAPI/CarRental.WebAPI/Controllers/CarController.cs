using CarRental.Application.Common.Interface;
using CarRental.Application.DTOs;
using CarRental.Application.DTOs.CarDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CarRental.WebAPI.Controllers
{
    [ApiController]
    public class CarController : Controller
    {
        private readonly ICars _cars;

        public CarController(ICars cars)
        {
            _cars = cars;
        }

        [HttpGet]
        [Route("/api/cars")]
        [Authorize(Roles = "Admin,Staff,User")]
        public async Task<ListCarResponse> GetCars()
        {
            var cars = await _cars.GetCars();
            return cars;
        }

        [HttpGet]
        [Route("/api/cars/{id}")]
        [Authorize(Roles = "Admin,Staff,User")]
        public async Task<SingleCarResponse> GetCarById(int id)
        {
            var car = await _cars.GetCarById(id);
            return car;
        }

        [HttpPost]
        [Route("/api/cars")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<MessageResponse> AddCar(AddCarRequestDTO car)
        {
            var message = await _cars.AddCar(car);
            return message;
        }

        [HttpPut]
        [Route("/api/cars/{id}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<MessageResponse> UpdateCar(int id, AddCarRequestDTO car)
        {
            var message = await _cars.UpdateCar(id, car);
            return message;
        }

        [HttpDelete]
        [Route("/api/cars/{id}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<MessageResponse> DeleteCar(int id)
        {
            var message = await _cars.DeleteCar(id);
            return message;
        }
    }
}
