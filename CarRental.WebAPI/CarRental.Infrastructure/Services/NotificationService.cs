using CarRental.Application.Common.Interface;
using CarRental.Application.DTOs.CarDTOs;
using CarRental.Domain.Entities;
using CarRental.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Infrastructure.Services
{
    public class NotificationService : INotification
    {
        private readonly IApplicationDBContext _dbContext;

        public NotificationService(IApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<MessageResponse> AddNotification(Notification notification)
        {


            Notification newNotification = new Notification
            {
                UserId = notification.UserId,
                Message = notification.Message,
                IsSeen = false,
            };
        

            _dbContext.Notification.Add(newNotification);
            await _dbContext.SaveChangesAsync();

            // Return a success message
            return new MessageResponse { message = "Notification added successfully." };
        }

        public async Task<MessageResponse> ReadNotification(string id)
        {
            // Find the notification by id
            List<Notification> notifications = await _dbContext.Notification.Where(n => n.UserId == id).ToListAsync();

            if (notifications.Count == 0)
            {
                return new MessageResponse { message = "No notification for this UserId." };
            }

            // Update the IsSeen property to true and save changes to the database
            notifications.ForEach(n => n.IsSeen = true);
            _dbContext.Notification.UpdateRange(notifications);
            await _dbContext.SaveChangesAsync();

            // Return a success message
            return new MessageResponse { message = "Notification marked as read." };
        }
        
        public async Task<List<Notification>> GetNotificationsByUserId(string id)
        {
            // Find all notifications for a user by UserId
            List<Notification> notifications = await _dbContext.Notification.Where(n => n.UserId == id).ToListAsync();

            // Return the list of notifications
            return notifications;
        }
    }

}

