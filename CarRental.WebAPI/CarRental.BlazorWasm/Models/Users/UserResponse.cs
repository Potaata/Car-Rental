using CarRental.BlazorWasm.Models.Items;

namespace CarRental.BlazorWasm.Models.Users
{
    public class UserResponse: SuccessResponse
    {
        public List<User> users { get; set; }
    }
}
