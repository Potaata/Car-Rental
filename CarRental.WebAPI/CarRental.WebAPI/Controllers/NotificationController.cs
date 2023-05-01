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

        public NotificationController(INotification notification)
        {
            _notification = notification;
        }


        [HttpGet]
        [Route("/api/user/notifications/{userId}")]
        public async Task<List<Notification>> GetNotifications(string userId)
        {
            return await _notification.GetNotificationsByUserId(userId);
        }

        [HttpPost]
        [Route("/api/user/notifications/{userId}")]
        public async Task<MessageResponse> ReadNotifications(string userId)
        {
            return await _notification.ReadNotification(userId);
        }

    }
}
