namespace CarRental.BlazorWasm.Models.Items
{
    public class Notification: TableItem
    {
        public string Message { get; set; }

        public Boolean IsSeen { get; set; }
    }
}
