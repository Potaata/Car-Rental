using CarRental.Application.Common.Interface;
using CarRental.Application.DTOs.CarDTOs;
using CarRental.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

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
        [Route("/api/user/notifications/")]
        public async Task<List<Notification>> GetNotifications()
        {
            Users user = await _authService.GetSessionUser(new List<string> { "User", "Admin", "Staff" });
            await _notification.ReadNotification(user.Id);
            return await _notification.GetNotificationsByUserId(user.Id);
        }
    }
}
