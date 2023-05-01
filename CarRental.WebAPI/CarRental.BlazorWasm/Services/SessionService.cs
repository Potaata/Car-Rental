using Newtonsoft.Json.Linq;
using Microsoft.JSInterop;
using CarRental.BlazorWasm.Models.Register;
using CarRental.BlazorWasm.Models.Login;
using CarRental.BlazorWasm.CustomException;
using CarRental.BlazorWasm.Pages;
using Newtonsoft.Json;
using System.Text;
using CarRental.BlazorWasm.Models;
using System.Net.Http.Headers;

namespace CarRental.BlazorWasm.Services
{
    public class SessionService
    {
        public string? Token { get; set; }
        public string? Username { get; set; }
        public string? Role { get; set; }

        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;

        public SessionService(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            _jsRuntime = jsRuntime;
            _httpClient = httpClient;
        }


        public async void SetSession(string token, string username, string role)
        {
            Token = token;
            Username = username;
            Role = role;

            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "$$token$$" ,Token);


             _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("", token);
        }

        public async Task Logout()
        {
            Token = null;
            Username = null;
            Role = null;
            await _jsRuntime.InvokeVoidAsync("localStorage.clear");
        }

        public async Task<bool> IsAuthenticated()
        {
            string token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "$$token$$");
            try
            {
                var bodyJSON = JsonConvert.SerializeObject(new TokenRequest { Token = token });
                var content = new StringContent(bodyJSON, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://localhost:7007/api/users/verify", content);
                LoginResponse loginResp = await ParseResponse<LoginResponse>(response);
                SetSession(loginResp.Token, loginResp.Username, loginResp.Role);
                return true;
            }
            catch (ApiException)
            {
                await Logout();
                return false;
            }
            catch (HttpRequestException)
            {
                await Logout();
                return false;
            }
        }


        public async Task<ResponseDTO> ParseResponse<ResponseDTO>(HttpResponseMessage response) where ResponseDTO : SuccessResponse
        {
            try
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var responseParsed = JsonConvert.DeserializeObject<ResponseDTO>(responseContent);
                    return responseParsed;
                }
                else
                {
                    ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);

                    if (errorResponse != null && errorResponse.ErrorMessage != null)
                    {
                        throw new ApiException(errorResponse.ErrorMessage);
                    }
                    throw new ApiException("");
                }
            }
            catch (HttpRequestException)
            {
                throw new ApiException("");
            }
        }
    }
}
