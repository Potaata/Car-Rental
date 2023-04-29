namespace CarRental.BlazorWasm.Models.Register
{
    public class LoginResponse: SuccessResponse
    {
        public string? Token { get; set; }
        public string? Username { get; set; }
        public string? Role { get; set; }
    }
}
