using CarRental.BlazorWasm.Models.Items;

namespace CarRental.BlazorWasm.Models.Staffs
{
    public class StaffResponse: SuccessResponse
    {
        public List<Staff> users { get; set; }
    }
}
