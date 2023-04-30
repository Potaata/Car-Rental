using CarRental.Application.Common.Interface;
using CarRental.Application.DTOs.CarDTOs;
using CarRental.Application.DTOs.RentHistoryDTOs;
using CarRental.Domain.Entities;
using CarRental.Domain.Enums;
using CarRental.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CarRental.Infrastructure.Services
{
    
    public class RentHistoryService : IRentHistory
    {
        private readonly IApplicationDBContext _dbcontext;

        public RentHistoryService(IApplicationDBContext dbContext, UserManager<Users> userManager)
        {
            _dbcontext = dbContext;
        }

        public async Task<MessageResponse> AddRentHistory(RentHistory rentHistory)
        {
            RentHistory newRentHistory = new RentHistory
            {
                UserId = rentHistory.UserId,
                CarId = rentHistory.CarId,
                FromDate = rentHistory.FromDate,
                ToDate = rentHistory.ToDate,
                Price = rentHistory.Price
                
            };

            _dbcontext.RentHistory.Add(newRentHistory);
            await _dbcontext.SaveChangesAsync();

            return new MessageResponse { message = "Request added successfully." };
        }
        
        public async Task<List<RentHistoryResponseDTO>> GetRentHistories()
        {
            var result = from rh in _dbcontext.RentHistory
                         join c in _dbcontext.Cars on rh.CarId equals c.Id
                         join u in _dbcontext.Users on rh.UserId equals u.Id
                         join ap in _dbcontext.Users on rh.ApprovedBy equals ap.Id into appr
                         from ap in appr.DefaultIfEmpty()
                         select new RentHistoryResponseDTO
                         {
                             Id = rh.Id,
                             UserName = u.UserName,
                             ApprovedBy = ap == null ? null : ap.UserName,
                             CarModel = c.Model,
                             CarNumberPlate = c.NumberPlate,
                             FromDate = rh.FromDate,
                             ToDate = rh.ToDate,
                             Status = rh.Status,
                             Price = rh.Price
                         };


            return await result.ToListAsync();
        }

        public async Task<MessageResponse> ApproveRequest(int rentId) 
        {
            var rentHistory = await _dbcontext.RentHistory.FindAsync(rentId);

            if (rentHistory == null)
            {
                return new MessageResponse {message = "Rent history not found" };
            }

            rentHistory.Status = StatusEnums.Approved;
            

            await _dbcontext.SaveChangesAsync();

            return new MessageResponse { message = "Rent history approved" };


        }

        public async Task<RentHistory> GetRentHistoryById(int id)
        {
            return await _dbcontext.RentHistory.FindAsync(id);
        }

        public async Task<List<RentHistory>> GetRentHistoriesByUserId(string id)
        {
            List<RentHistory> rentHistories = await _dbcontext.RentHistory.Where(x => x.UserId == id).ToListAsync();

            if (rentHistories == null)
            {
                throw new ApiException("Invalid ID!");
            }

            return rentHistories;
        }
        
        public async Task<MessageResponse> DeclineRequest(int id) 
        {
            var rentHistory = await _dbcontext.RentHistory.FindAsync(id);

            if (rentHistory == null)
            {
                return new MessageResponse { message = "Rent history not found" };
            }

            rentHistory.Status = StatusEnums.Denied;


            await _dbcontext.SaveChangesAsync();

            return new MessageResponse { message = "Rent history approved" };
        }

        public async Task<MessageResponse> CarTaken(int rentId)
        {
            var rentHistory = await _dbcontext.RentHistory.FindAsync(rentId);

            if (rentHistory == null)
            {
                return new MessageResponse { message = "Rent history not found" };
            }

            rentHistory.Status = StatusEnums.Rented;

            await _dbcontext.SaveChangesAsync();

            return new MessageResponse { message = "Rent history approved" };
        }

    }
    
}
