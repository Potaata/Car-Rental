using CarRental.BlazorWasm.Models;
using CarRental.BlazorWasm.Models.Register;

namespace CarRental.BlazorWasm.Services
{
    public class LoginService
    {
        private readonly ApiService _apiService;
        private readonly SessionService _sessionService;

        string EndPoint = "/api/login";

        public LoginService(ApiService apiService, SessionService sessionService)
        {
            _apiService = apiService;
            _sessionService = sessionService;
        }

        public async void Login(string email, string password)
        {
            LoginRequest body = new LoginRequest
            {
                Email = email,
                RawPassword = password
            };
            
            LoginResponse res = await _apiService.POST<LoginRequest, LoginResponse>(EndPoint, body);

            _sessionService.SetSession(res.Token, res.Username, res.Role);
        }
    }
}
