using CarRental.BlazorWasm.Models;
using CarRental.BlazorWasm.Models.Register;
using Microsoft.AspNetCore.Components;

namespace CarRental.BlazorWasm.Services
{
    public class UserService
    {
        private readonly ApiService _apiService;

        public UserService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<RegisterResponse> Register(string username, string email, string password, string phoneNum)
        {
            RegisterRequest req = new RegisterRequest
            {
                Email = email,
                Username = username,
                RawPassword = password,
                PhoneNumber = phoneNum
            };
            
            BaseResponse response = await _apiService.POST<RegisterRequest, RegisterResponse>("/api/users/create-user", req);
                    
            return response.GetResponse<RegisterResponse>();
        }
    }
}

