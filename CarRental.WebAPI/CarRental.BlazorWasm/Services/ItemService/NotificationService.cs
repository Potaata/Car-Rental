using CarRental.BlazorWasm.Models;
using CarRental.BlazorWasm.Models.Items;

namespace CarRental.BlazorWasm.Services.ItemService
{
    public class NotificationService {
        private readonly ApiService _apiService;

        public string EndPoint = "/api/notifications";
        public NotificationService(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<List<Notification>> GetNotification()
        {
            NotificationResponse notifications = await _apiService.GET<NotificationResponse>(EndPoint);

            return notifications.notifications;
        }
    }
}
