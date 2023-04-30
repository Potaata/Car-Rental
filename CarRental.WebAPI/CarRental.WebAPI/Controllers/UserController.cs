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
        private readonly IAuthService _authService;

        public UserController(IUsers users, IAuthService authService)
        {
            _users = users;
            _authService = authService;
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

        [HttpPost]
        [Route("/api/users/verify")]
        public async Task<UserLoginResponseDTO> VerifyToken(TokenRequestDTO tokenReq)
        {
            UserLoginResponseDTO user = await _authService.ValidateToken(tokenReq.Token);
            return user;
        }
    }
}
