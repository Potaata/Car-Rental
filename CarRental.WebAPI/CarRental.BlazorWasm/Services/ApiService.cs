using System.Text;
using System.Text.Json;

namespace CarRental.BlazorWasm.Services
{
    public class ApiService
    {

        public HttpClient _httpClient;
        private static string API_URL = "https://localhost:7007";

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO> POST<RequestDTO, ResponseDTO>(string endpoint, RequestDTO body)
        {
            string fullQualifiedEndpoint = API_URL + endpoint;
            var bodyJSON = JsonSerializer.Serialize(body);
            var content = new StringContent(bodyJSON, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(fullQualifiedEndpoint, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseParsed = JsonSerializer.Deserialize<ResponseDTO>(responseContent);
            return responseParsed;
        }
    }
}
