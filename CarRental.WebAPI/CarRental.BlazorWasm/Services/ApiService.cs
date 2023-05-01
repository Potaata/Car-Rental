using CarRental.BlazorWasm.Models;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Text;
using CarRental.BlazorWasm.CustomException;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Headers;

namespace CarRental.BlazorWasm.Services
{
    public class ApiService
    {

        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navManager;
        private readonly SessionService _sessService;
        private static string API_URL = "https://localhost:7007";


        // Failed To Fetch API. Make sure API project is running and the endpoint is accessible.
        public static string FailedToFetchError = "Failed to access server!";

        // The API did not return 200 OK but did not send any errorMessage either. Please check the endpoint.
        public static string NoErrorMessage = "Something went wrong!";

        public ApiService(HttpClient httpClient, NavigationManager navManager, SessionService sessionService)
        {
            _httpClient = httpClient;
            _navManager = navManager;
            _sessService = sessionService;
        }

        public async Task<ResponseDTO> GET<ResponseDTO>(string endpoint) where ResponseDTO : SuccessResponse
        {
            string fullQualifiedEndpoint = API_URL + endpoint;
            var response = await _httpClient.GetAsync(fullQualifiedEndpoint);
            return await ParseResponse<ResponseDTO>(response);
        }

        public async Task<ResponseDTO> POST<RequestDTO, ResponseDTO>(string endpoint, RequestDTO body) where ResponseDTO : SuccessResponse
        {
            string fullQualifiedEndpoint = API_URL + endpoint;
            var bodyJSON = JsonConvert.SerializeObject(body);
            var content = new StringContent(bodyJSON, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(fullQualifiedEndpoint, content);
            return await ParseResponse<ResponseDTO>(response);
        }

        public async Task<ResponseDTO> DELETE<ResponseDTO>(string endpoint) where ResponseDTO : SuccessResponse
        {
            string fullQualifiedEndpoint = API_URL + endpoint;
            var response = await _httpClient.DeleteAsync(fullQualifiedEndpoint);
            return await ParseResponse<ResponseDTO>(response);
        }

        public async Task<ResponseDTO> PUT<RequestDTO, ResponseDTO>(string endpoint, RequestDTO body) where ResponseDTO : SuccessResponse
        {
            string fullQualifiedEndpoint = API_URL + endpoint;
            var bodyJSON = JsonConvert.SerializeObject(body);
            var content = new StringContent(bodyJSON, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(fullQualifiedEndpoint, content);
            return await ParseResponse<ResponseDTO>(response);
        }

        public async Task<UrlResponse> FileUpload(MultipartFormDataContent content) {
            string fullQualifiedEndpoint = API_URL + "/api/FileUpload";
            var response = await _httpClient.PostAsync(fullQualifiedEndpoint, content);
            return await ParseResponse<UrlResponse>(response);
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
                    throw new ApiException(NoErrorMessage);
                }
            }
            catch (HttpRequestException e)
            {
                //throw new ApiException(FailedToFetchError);
                _navManager.NavigateTo("404", true);
                throw new Exception("This exception should not occur.");
            }
        }
    }
}

