using CarRental.BlazorWasm.Models.Items;
using CarRental.BlazorWasm.Models.Staffs;

namespace CarRental.BlazorWasm.Models.Users
{
    public class UserRequest: TableItemRequest
    {
        public string UserName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public UserRequest GetDefault()
        {
            return new UserRequest();
        }
    }
}
