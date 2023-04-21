using CarRental.BlazorWasm.Models;
using Microsoft.AspNetCore.Components;

namespace CarRental.BlazorWasm.Services
{
    public class UserService
    {
        [Inject]
        public ApiService _apiService { get; set; }

        public UserService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<RegisterResponse> register(string username, string email, string password, string phoneNum)
        {
            RegisterRequest req = new RegisterRequest
            {
                Email = email,
                Username = username,
                RawPassword = password,
                PhoneNumber = phoneNum
            };
            var response = await _apiService.POST<RegisterRequest, RegisterResponse>("/api/users/create-user", req);
            return response;
        }
    }
}
