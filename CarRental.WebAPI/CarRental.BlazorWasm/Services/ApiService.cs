using CarRental.BlazorWasm.Models;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Text;

namespace CarRental.BlazorWasm.Services
{
    public class ApiService
    {

        private readonly HttpClient _httpClient;
        private static string API_URL = "https://localhost:7007";

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BaseResponse> GET<ResponseDTO>(string endpoint) where ResponseDTO : SuccessResponse
        {
            string fullQualifiedEndpoint = API_URL + endpoint;
            var response = await _httpClient.GetAsync(fullQualifiedEndpoint);
            return await parseResponse<ResponseDTO>(response);
        }

        public async Task<BaseResponse> POST<RequestDTO, ResponseDTO>(string endpoint, RequestDTO body) where ResponseDTO : SuccessResponse
        {
            string fullQualifiedEndpoint = API_URL + endpoint;
            var bodyJSON = JsonConvert.SerializeObject(body);
            var content = new StringContent(bodyJSON, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(fullQualifiedEndpoint, content);
            return await parseResponse<ResponseDTO>(response);
        }

        public async Task<BaseResponse> DELETE<ResponseDTO>(string endpoint) where ResponseDTO : SuccessResponse
        {
            string fullQualifiedEndpoint = API_URL + endpoint;
            var response = await _httpClient.DeleteAsync(fullQualifiedEndpoint);
            return await parseResponse<ResponseDTO>(response);
        }

        public async Task<BaseResponse> PUT<RequestDTO, ResponseDTO>(string endpoint, RequestDTO body) where ResponseDTO : SuccessResponse
        {
            string fullQualifiedEndpoint = API_URL + endpoint;
            var bodyJSON = JsonConvert.SerializeObject(body);
            var content = new StringContent(bodyJSON, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(fullQualifiedEndpoint, content);
            return await parseResponse<ResponseDTO>(response);
        }

        public async Task<BaseResponse> parseResponse<ResponseDTO>(HttpResponseMessage response) where ResponseDTO : SuccessResponse
        {
            try
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var responseParsed = JsonConvert.DeserializeObject<ResponseDTO>(responseContent);

                    // TODO: Refactor and remove unnecessary classes: BaseResponse, ErrorResponse
                    BaseResponse res = new BaseResponse
                    {
                        Response = responseParsed
                    };
                    return res;
                }
                else
                {
                    ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                    BaseResponse res = new BaseResponse
                    {
                        ErrorMessage = errorResponse.ErrorMessage
                    };

                    if (errorResponse != null && errorResponse.ErrorMessage != null)
                    {
                        return res;
                    }
                    return BaseResponse.NoErrorMessage;
                }
            }
            catch (HttpRequestException e)
            {
                return BaseResponse.FailedToFetchError;
            }
        }
    }
}

