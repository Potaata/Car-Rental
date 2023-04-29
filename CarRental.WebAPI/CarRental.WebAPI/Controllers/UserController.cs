using CarRental.Application.Common.Interface;
using CarRental.Application.DTOs;
using CarRental.Application.DTOs.CarDTOs;
using CarRental.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<MessageResponse> RegisterUser(UserRegisterRequestDTO userRequestDTO)
        {
            var registeredUser = await _users.RegisterUser(userRequestDTO);
            return registeredUser;
        }
        
        [HttpPost]
        [Route("/api/users/login")]
        public async Task<UserLoginResponseDTO> LoginUser(UserLoginRequestDTO userRequestDTO)
        {
            UserLoginResponseDTO res = await _users.LoginUser(userRequestDTO);
            return res;
        }
    }
}
