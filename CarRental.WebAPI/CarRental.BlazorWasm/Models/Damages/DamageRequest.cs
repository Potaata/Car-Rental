using CarRental.BlazorWasm.Models.Items;

namespace CarRental.BlazorWasm.Models.Damages
{
    public class DamageRequest: TableItemRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DamageRequest GetDefault()
        {
            return new DamageRequest();
        }
    }
}
