using CarRental.BlazorWasm.Models.Items;

namespace CarRental.BlazorWasm.Models.Staffs
{
    public class StaffRequest: TableItemRequest
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set; }

        public StaffRequest GetDefault()
        {
            return new StaffRequest();
        }
    }
}
