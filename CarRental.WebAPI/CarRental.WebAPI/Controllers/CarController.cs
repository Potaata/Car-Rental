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
        private readonly IAuthService _authService;

        public CarController(ICars cars, IAuthService authService)
        {
            _cars = cars;
            _authService = authService;
        }

        [HttpGet]
        [Route("/api/cars")]
        public async Task<ListCarResponse> GetCars()
        {
            await _authService.GetSessionUser(new List<string> { "Admin", "Staff", "User" });
            var cars = await _cars.GetCars();
            return cars;
        }

        [HttpGet]
        [Route("/api/cars/{id}")]
        public async Task<SingleCarResponse> GetCarById(int id)
        {
            await _authService.GetSessionUser(new List<string> { "Admin", "Staff", "User" });
            var car = await _cars.GetCarById(id);
            return car;
        }

        [HttpPost]
        [Route("/api/cars")]
        public async Task<MessageResponse> AddCar(AddCarRequestDTO car)
        {
            await _authService.GetSessionUser(new List<string> { "Admin", "Staff" });
            var message = await _cars.AddCar(car);
            return message;
        }

        [HttpPut]
        [Route("/api/cars/{id}")]
        public async Task<MessageResponse> UpdateCar(int id, AddCarRequestDTO car)
        {
            await _authService.GetSessionUser(new List<string> { "Admin", "Staff" });
            var message = await _cars.UpdateCar(id, car);
            return message;
        }

        [HttpDelete]
        [Route("/api/cars/{id}")]
        public async Task<MessageResponse> DeleteCar(int id)
        {
            await _authService.GetSessionUser(new List<string> { "Admin", "Staff" });
            var message = await _cars.DeleteCar(id);
            return message;
        }
    }
}
