namespace CarRental.BlazorWasm.Models.Items
{
    public class Rent: TableItem
    {
        public string CarModel { get; set; }

        public DateOnly RentDate { get; set; }

        public DateOnly ReturnDate { get; set; }

        public string AuthorizedBy { get; set; }

        public float RentPrice { get; set; } 

    }
}
