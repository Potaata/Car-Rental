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

        public async Task<BaseResponse> POST<RequestDTO, ResponseDTO>(string endpoint, RequestDTO body) where ResponseDTO: SuccessResponse
        {
            try
            {
                string fullQualifiedEndpoint = API_URL + endpoint;
                var bodyJSON = JsonConvert.SerializeObject(body);
                var content = new StringContent(bodyJSON, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(fullQualifiedEndpoint, content);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                { 
                    var responseParsed = JsonConvert.DeserializeObject<ResponseDTO>(responseContent);

                    BaseResponse res = new BaseResponse
                    {
                        Response = responseParsed
                    };
                    return res;
                }
                else
                {
                    ApiError apiError = JsonConvert.DeserializeObject<ApiError>(responseContent);
                    ErrorResponse errorResponse = new ErrorResponse(apiError.errors);
                    BaseResponse res = new BaseResponse
                    {
                        ErrorMessage = errorResponse.ErrorMessage
                    };
                    
                    if(errorResponse != null && errorResponse.ErrorMessage != null)
                    {
                        return BaseResponse.NoErrorMessage;
                    }
                    return res;
                }
            }
            catch (HttpRequestException e)
            {
                return BaseResponse.FailedToFetchError;
            }
        }
    }
}
