using CarRental.BlazorWasm.Models.Items;

namespace CarRental.BlazorWasm.Models.Damages
{
    public class CreateDamageRequest
    {

        public string Description { get; set; }

        public int RentId { get; set; }

        public CreateDamageRequest GetDefault()
        {
            return new CreateDamageRequest();
        }
    }
}
