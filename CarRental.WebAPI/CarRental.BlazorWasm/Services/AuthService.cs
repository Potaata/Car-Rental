using CarRental.BlazorWasm.Models;
using CarRental.BlazorWasm.Models.Register;
using Microsoft.AspNetCore.Components;
using System.Formats.Asn1;

namespace CarRental.BlazorWasm.Services
{
    public class AuthService
    {
        private readonly ApiService _apiService;
        private readonly SessionService _sessionService;

        public AuthService(ApiService apiService, SessionService sessService)
        {
            _apiService = apiService;
            _sessionService = sessService;
        }

        public async Task<RegisterResponse> Register(string name, string username, string email, string address, string password, string phoneNum)
        {
            RegisterRequest req = new RegisterRequest
            {
                Name = name,
                Email = email,
                Address = address,
                Username = username,
                RawPassword = password,
                PhoneNumber = phoneNum
            };
            
            return await _apiService.POST<RegisterRequest, RegisterResponse>("/api/users/register", req);
        }

        public async Task<bool> Login(string email, string password)
        {
            LoginRequest req = new LoginRequest
            {
                Email = email,
                RawPassword = password
            };

            LoginResponse resp = await _apiService.POST<LoginRequest, LoginResponse>("/api/users/login", req);

            await _sessionService.SetSession(resp.Token, resp.Username, resp.Role);
            return true;
        }
    }
}

