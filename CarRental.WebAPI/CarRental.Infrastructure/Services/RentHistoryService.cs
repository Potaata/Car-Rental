using CarRental.Application.Common.Interface;
using CarRental.Application.DTOs.CarDTOs;
using CarRental.Application.DTOs.RentHistoryDTOs;
using CarRental.Domain.Entities;
using CarRental.Domain.Enums;
using CarRental.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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
        private readonly IAuthService _authService;


        public RentHistoryService(IApplicationDBContext dbContext, UserManager<Users> userManager, IAuthService authService)
        {
            _dbcontext = dbContext;
            _authService = authService;
        }

        public async Task<MessageResponse> AddRentHistory(RentHistory newRent)
        {
            if (DateTime.Compare(newRent.ToDate, newRent.FromDate) < 0)
            {
                throw new ApiException("To Date is earlier than From Date");
            }
            
            bool isOverlap = await _dbcontext.RentHistory
                         .Where(rh => rh.CarId == newRent.CarId &&
                                      (rh.Status == StatusEnums.Approved || rh.Status == StatusEnums.Rented) &&
                                      rh.FromDate <= newRent.ToDate &&
                                      rh.ToDate >= newRent.FromDate)
                         .AnyAsync();

            var unpaidDamage = from rh in _dbcontext.RentHistory
                           join dr in _dbcontext.DamageRequest on rh.Id equals dr.RentID
                           where (dr.isPaid == false && rh.UserId == newRent.UserId)
                           select new DamageRequest
                           {
                               Id = dr.Id,
                               Description = dr.Description,
                               RentID = dr.RentID,
                               Cost = dr.Cost,
                               isPaid = dr.isPaid


                           };

            if (unpaidDamage.Count() > 0) {
                throw new ApiException("Pay your damage cost first");
            }
            
            if (isOverlap)
            {
                return new MessageResponse { message = "Rent history overlaps with existing entries." };
            }
            else
            {
                

                _dbcontext.RentHistory.Add(newRent);
                await _dbcontext.SaveChangesAsync();
                return new MessageResponse { message = "Rent history added successfully." };
            }
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
            var sessionUser = await _authService.GetSessionUser();
            var rentHistory = await _dbcontext.RentHistory.FindAsync(rentId);
            if (rentHistory == null)
            {
                return new MessageResponse { message = "Rent request not found." };
            }

            if (rentHistory.Status != StatusEnums.Pending)
            {
                return new MessageResponse { message = "Rent request not pending." };
            }


            rentHistory.Status = StatusEnums.Approved;
            rentHistory.ApprovedBy = sessionUser.Id;
            _dbcontext.RentHistory.Update(rentHistory);
            await _dbcontext.SaveChangesAsync();

            return new MessageResponse { message = "Rent request approved successfully." };


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
            var sessionUser = await _authService.GetSessionUser();
            var rentHistory = await _dbcontext.RentHistory.FindAsync(id);
            if (rentHistory == null)
            {
                return new MessageResponse { message = "Rent request not found." };
            }

            if (rentHistory.Status != StatusEnums.Pending)
            {
                return new MessageResponse { message = "Rent request not pending." };
            }



            rentHistory.Status = StatusEnums.Denied;
            rentHistory.ApprovedBy = sessionUser.Id;
            _dbcontext.RentHistory.Update(rentHistory);
            await _dbcontext.SaveChangesAsync();

            return new MessageResponse { message = "Rent request denied successfully." };
        }

        public async Task<MessageResponse> CarTaken(int rentId)
        {
            var rentHistory = await _dbcontext.RentHistory.FindAsync(rentId);
            if (rentHistory == null)
            {
                return new MessageResponse { message = "Rent request not found." };
            }

            if (rentHistory.Status != StatusEnums.Approved)
            {
                return new MessageResponse { message = "Rent request not approved yet." };
            }

            rentHistory.Status = StatusEnums.Rented;
            _dbcontext.RentHistory.Update(rentHistory);
            await _dbcontext.SaveChangesAsync();

            return new MessageResponse { message = "Car marked taken." };
        }

        public async Task<MessageResponse> CancelCarRequest(int rentId) {

            var rentHistory = await _dbcontext.RentHistory.FindAsync(rentId);
            if (rentHistory == null)
            {
                return new MessageResponse { message = "Rent request not found." };
            }

            if (rentHistory.Status == StatusEnums.Rented || rentHistory.Status == StatusEnums.Returned)
            {
                return new MessageResponse { message = "Rent request cannot be canceled." };
            }

            rentHistory.Status = StatusEnums.Canceled;
            _dbcontext.RentHistory.Update(rentHistory);
            await _dbcontext.SaveChangesAsync();

            return new MessageResponse { message = "Rent request cancelled successfully." };
        }
    }
    
}
