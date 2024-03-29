﻿using CarRental.Application.Common.Interface;
using CarRental.Application.DTOs;
using CarRental.Application.DTOs.CarDTOs;
using CarRental.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Infrastructure.Exceptions;

namespace CarRental.Infrastructure.Services
{
    public class DamageRequestService : IDamageRequest
    {
        private readonly IApplicationDBContext _dbcontext;

        public DamageRequestService(IApplicationDBContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<MessageResponse> AddDamageRequest(DamageRequest damageRequest)
        {
            if (string.IsNullOrEmpty(damageRequest.Description))
                throw new ApiException("Description cannot be empty.");
            _dbcontext.DamageRequest.Add(damageRequest);
            await _dbcontext.SaveChangesAsync();
            return new MessageResponse { message = "Damage Request Added to the database." };
        }

        public async Task<MessageResponse> QuoteCost(int damageId, float quoteCost)
        {
            var damageRequest = await _dbcontext.DamageRequest.FindAsync(damageId);

            damageRequest.Cost = quoteCost;
            
            await _dbcontext.SaveChangesAsync();

            // add notification

            var userId = _dbcontext.DamageRequest
    .Join(_dbcontext.RentHistory, dr => dr.RentID, rh => rh.Id, (dr, rh) => new { DamageRequest = dr, RentHistory = rh })
    .Where(joined => joined.DamageRequest.RentID == joined.RentHistory.Id)
    .Select(joined => joined.RentHistory.UserId).FirstOrDefault();

            var notification = new Notification
            {
                UserId = userId,
                Message = "Your damage request has been quoted."
            };

            _dbcontext.Notification.Add(notification);
            await _dbcontext.SaveChangesAsync();


            return new MessageResponse { message = "Damage Cost updated successfully" };

        }

        public async Task<MessageResponse> MarkPaid(int id)
        {
            var damageRequest = await _dbcontext.DamageRequest.FindAsync(id);

            damageRequest.isPaid = true;

            await _dbcontext.SaveChangesAsync();

            return new MessageResponse { message = "Damage Request Marked Paid" };
        }

        public async Task<DamageRequestListResponseDTO> GetAllRequests() {

            var requestsDB = from dr in _dbcontext.DamageRequest
                           join rh in _dbcontext.RentHistory on dr.RentID equals rh.Id
                           join c in _dbcontext.Cars on rh.CarId equals c.Id
                           select new DamageRequestJoinedDTO
                           {
                               CarModel = c.Model,
                               Description = dr.Description,

                               Id = dr.Id,
                               Cost = dr.Cost,
                               IsPaid = dr.isPaid
                           };
            var requests = await requestsDB.ToListAsync();
            return new DamageRequestListResponseDTO { damageRequests = requests};
            
        }
    }
}
