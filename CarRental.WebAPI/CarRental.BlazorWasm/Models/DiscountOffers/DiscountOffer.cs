namespace CarRental.BlazorWasm.Models.DiscountOffers
{
    public class DiscountOffer
    {
        public string? Name { get; set; }
        public float DiscountPercent { get; set; }
        public DateTime ValidTill { get; set; }
    }
}
