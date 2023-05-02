namespace CarRental.BlazorWasm.Models.Damages
{
    public class DamageTableItem
    {
        public int Id { get; set; }
        public string? CarModel { get; set; }
        public string? Description { get; set; }
        public bool IsPaid { get; set; }
        public float? Cost { get; set; }
    }
}
