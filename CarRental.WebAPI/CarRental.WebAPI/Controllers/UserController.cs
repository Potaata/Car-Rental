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

        [HttpGet]
        [Route("/api/users/regular")]
        public async Task<UserListResponseDTO> GetRegularUsers()
        {
            await _authService.GetSessionUser(new List<string> { "Admin", "Staff" });
            return await _users.GetRegularUsers();
        }

        [HttpGet]
        [Route("/api/users/inactive")]
        public async Task<UserListResponseDTO> GetInactiveUsers()
        {
            await _authService.GetSessionUser(new List<string> { "Admin", "Staff" });
            return await _users.GetInactiveUsers();
        }


        [HttpGet]
        [Route("/api/users/")]
        public async Task<UserListResponseDTO> GetAllUsers()
        {
            await _authService.GetSessionUser(new List<string> { "Admin", "Staff" });
            return await _users.GetAllUsers();
        }

        [HttpPost]
        [Route("/api/users/change-password")]
        public async Task<MessageResponse> ChangePassword(ChangePasswordDTO passwords)
        {
            await _authService.GetSessionUser(new List<string> { "Admin", "Staff" , "User" });
            return await _users.ChangePassword(passwords);
        }


        [HttpGet]
        [Route("/api/users/isRegular")]
        public async Task<IsRegularUserResponseDTO> CheckRegularUser()
        {
            Users user = await _authService.GetSessionUser(new List<string> { "Admin", "User", "Staff" });
            var regularUsers = (await GetRegularUsers()).users;
            bool isRegular = regularUsers.Where(x => x.Id == user.Id).ToList().Count > 0;
            return new IsRegularUserResponseDTO { IsRegular = isRegular };
        }

        [HttpGet]
        [Route("/api/users/role")]
        public async Task<UserRoleResponseDTO> GetUserRole()
        {
            await _authService.GetSessionUser(new List<string> { "Admin", "Staff", "User" });
            string role = await _authService.GetUserRole();
            return new UserRoleResponseDTO { Role = role };
        }

        [HttpPost]
        [Route("/api/users/document")]
        public async Task<MessageResponse> UpdatePhotoUrl(UpdateDocumentDTO document)
        {
            Users user = await _authService.GetSessionUser(new List<string> { "Admin", "User", "Staff" });
            return await _users.UpdatePhotoUrl(user.Id,document.URL);
        }

        [HttpGet]
        [Route("/api/staffs")]
        public async Task<UserListResponseDTO> GetAllStaffs()
        {
            Users user = await _authService.GetSessionUser(new List<string> { "Admin", "Staff" });
            return new UserListResponseDTO { users = await _users.GetAllStaffs() };
        }
        [Route("/api/admin/add-staff")]
        public async Task<MessageResponse> AddStaff(UserRegisterRequestDTO userRequestDTO)
        {
            var registeredUser = await _users.AddStaff(userRequestDTO);
            return registeredUser;
        }
        
        [HttpPost]
        [Route("/api/admin/add-admin")]
        public async Task<MessageResponse> AddAdmin(UserRegisterRequestDTO userRequestDTO)
        {
            var registeredUser = await _users.AddAdmin(userRequestDTO);
            return registeredUser;
        }






    }
}


