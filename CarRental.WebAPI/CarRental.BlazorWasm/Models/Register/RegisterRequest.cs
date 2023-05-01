namespace CarRental.BlazorWasm.Models.Register
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string RawPassword { get; set; }
        public string Name { get; internal set; }
        public string Address { get; internal set; }
    }
}
