using CarRental.Application.Common.Interface;
using CarRental.Application.DTOs.CarDTOs;
using CarRental.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using CarRental.Application.DTOs;

namespace CarRental.WebAPI.Controllers
{
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly INotification _notification;
        private readonly IAuthService _authService;

        public NotificationController(INotification notification, IAuthService authService)
        {
            _notification = notification;
            _authService = authService;
        }


        [HttpGet]
        [Route("/api/user/notifications")]
        public async Task<NotificationListResponseDTO> GetNotifications()
        {
            Users user = await _authService.GetSessionUser(new List<string> { "User", "Admin", "Staff" });

            var notifications = await _notification.GetNotificationsByUserId(user.Id);
            var response = new NotificationListResponseDTO { notifications = notifications };

            return response;
        }
    }
}
