using CarRental.Application.Common.Interface;
using CarRental.Application.DTOs;
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

    }
}
