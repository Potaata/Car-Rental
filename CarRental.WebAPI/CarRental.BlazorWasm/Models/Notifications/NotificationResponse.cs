namespace CarRental.BlazorWasm.Models.Items
{
    public class NotificationResponse: SuccessResponse
    {
        public List<Notification> notifications { get; set; }
    }
}
