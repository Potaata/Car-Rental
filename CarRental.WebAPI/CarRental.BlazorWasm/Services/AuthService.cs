using CarRental.BlazorWasm.Models;
using CarRental.BlazorWasm.Models.Register;
using Microsoft.AspNetCore.Components;
using System.Formats.Asn1;

namespace CarRental.BlazorWasm.Services
{
    public class AuthService
    {
        private readonly ApiService _apiService;

        public AuthService(ApiService apiService)
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
            
            BaseResponse response = await _apiService.POST<RegisterRequest, RegisterResponse>("/api/users/register", req);
                    
            return response.GetResponse<RegisterResponse>();
        }

        public async Task<string> Login(string email, string password)
        {
            LoginRequest req = new LoginRequest
            {
                Email = email,
                RawPassword = password
            };

            BaseResponse br = await _apiService.POST<LoginRequest, LoginResponse>("/api/users/login", req);
            LoginResponse resp = br.GetResponse<LoginResponse>();

            return resp.Token;
        }
    }
}

