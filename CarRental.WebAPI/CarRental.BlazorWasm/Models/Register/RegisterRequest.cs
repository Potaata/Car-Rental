namespace CarRental.BlazorWasm.Models.Register
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string RawPassword { get; set; }
    }
}
