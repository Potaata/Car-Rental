namespace CarRental.BlazorWasm.Services
{
    public class SessionService
    {
        public string? Token { get; set; }
        public string? Username { get; set; }
        public string? Role { get; set; }

        public void SetSession(string token, string username, string role)
        {
            Token = token;
            Username = username;
            Role = role;
        }

        public void Logout()
        {
            Token = null;
            Username = null;
            Role = null;
        }

        public bool IsAuthenticated()
        {
            return !string.IsNullOrEmpty(Token);
        }
    }
}
