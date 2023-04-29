using CarRental.BlazorWasm.Models.Items;

namespace CarRental.BlazorWasm.Models.Staffs
{
    public class StaffResponse: SuccessResponse
    {
        public List<Staff> staffs { get; set; }
    }
}
