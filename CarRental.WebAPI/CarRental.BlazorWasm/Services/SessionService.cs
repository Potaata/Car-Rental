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
using Microsoft.AspNetCore.Components;
using System.Net;

namespace CarRental.BlazorWasm.Services
{
    public class SessionService
    {
        public string? Token { get; set; }
        public string? Username { get; set; }
        public string? Role { get; set; }

        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navManager;
        public SessionService(IJSRuntime jsRuntime, HttpClient httpClient, NavigationManager navManager)
        {
            _jsRuntime = jsRuntime;
            _httpClient = httpClient;
            _navManager = navManager;
        }


        public async Task SetSession(string token, string username, string role)
        {
            Token = token;
            Username = username;
            Role = role;

            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "$$token$$" ,Token);
             AuthenticationHeaderValue v = new AuthenticationHeaderValue("Bearer", token ?? "NOTOKEN");
            _httpClient.DefaultRequestHeaders.Authorization = v;
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
                var bodyJSON = JsonConvert.SerializeObject(new TokenRequest { Token = token ?? "NOTOKEN" });
                var content = new StringContent(bodyJSON, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://localhost:7007/api/users/verify", content);
                LoginResponse loginResp = await ParseResponse<LoginResponse>(response);
                await SetSession(loginResp.Token, loginResp.Username, loginResp.Role);
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
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        _navManager.NavigateTo("404", true);
                        throw new Exception("This exception should not occur.");
                    }
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
                _navManager.NavigateTo("404", true);
                throw new Exception("This exception should not occur.");
            }
        }
    }
}
