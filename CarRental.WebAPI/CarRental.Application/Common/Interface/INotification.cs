using CarRental.Application.DTOs.CarDTOs;
using CarRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Application.Common.Interface
{
    public interface INotification
    {
        
        public Task<MessageResponse> AddNotification(Notification notification);

        public Task<MessageResponse> ReadNotification(int id);

        public Task<List<Notification>> GetNotificationsByUserId(int id);
    }
}
