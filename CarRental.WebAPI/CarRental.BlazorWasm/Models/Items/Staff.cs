namespace CarRental.BlazorWasm.Models.Items
{
    public class Staff: TableItem
    {
        public string Id { get; set; }
        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Role { get; set; }
    }
}
