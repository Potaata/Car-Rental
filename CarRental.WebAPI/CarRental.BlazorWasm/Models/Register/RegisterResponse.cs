namespace CarRental.BlazorWasm.Models.Register
{
    public class RegisterResponse : SuccessResponse
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
    }
}
