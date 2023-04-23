using CarRental.Application.Common.Interface;
using CarRental.Application.DTOs;
using CarRental.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebAPI.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsers _users;

        public UserController(IUsers users)
        {
            _users = users;
        }


        [HttpPost]
        [Route("/api/users/register")]
        public async Task<UserRegisterRequestDTO> AddUsers(UserRegisterRequestDTO userRequestDTO)
        {
            var registeredUser = await _users.AddUsers(userRequestDTO);
            return registeredUser;
        }

        [HttpPost]
        [Route("/api/cars")]
        public async Task<CarResponse> GetCars(UserRegisterRequestDTO userRequestDTO)
        {
            List<Cars> cars = new List<Cars>
            {
                new Cars
                {
                    Color="Red",
                    Id="ID",
                    Model="asdf",
                    NumberPlate="asdf",
                    Price=123.45f
                }
            };
            CarResponse r = new CarResponse
            {
                cars = cars
            };

            return r;
        }
    }
}
