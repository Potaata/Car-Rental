namespace CarRental.BlazorWasm.Models.Items
{
    public class Damage: TableItem
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int RepairAmount { get; set; }
    }
}
