using CarRental.Application.Common.Interface;
using CarRental.Application.DTOs.CarDTOs;
using CarRental.Application.DTOs.RentHistoryDTOs;
using CarRental.Domain.Entities;
using CarRental.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental.WebAPI.Controllers
{
    [ApiController]
    public class RentHistoryController : ControllerBase
    {
        private readonly IRentHistory _rentHistory;
        private readonly IApplicationDBContext _dbcontext;
        private readonly IAuthService _authService;


        public RentHistoryController(IRentHistory rentHistory, IApplicationDBContext dbcontext, IAuthService authService)
        {
            _rentHistory = rentHistory;
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("/api/admin/renthistory")]
        public async Task<List<RentHistoryResponseDTO>> GetRentHistories()
        {
            return await _rentHistory.GetRentHistories();
        }

        [HttpPost]
        [Route("api/user/add-rent")]
        public async Task<MessageResponse> AddRentHistory(RentHistory newRent)
        {
            return await _rentHistory.AddRentHistory(newRent);
        }

        [HttpPost]
        [Route("api/admin/approve-request/{rentId}")]
        public async Task<MessageResponse> ApproveRequest(int rentId)
        {
            return await _rentHistory.ApproveRequest(rentId);   
        }

        [HttpPost]
        [Route("api/admin/deny-request/{rentId}")]
        public async Task<MessageResponse> DenyRequest(int rentId)
        {
            return await _rentHistory.DeclineRequest(rentId);
        }

        [HttpGet]
        [Route("api/user/rent-history/{userId}")]
        public async Task<List<RentHistory>> GetRentHistoriesByUserId(string userId)
        {
            return await _rentHistory.GetRentHistoriesByUserId(userId);
        }

        [HttpPost]
        [Route("api/admin/car-taken/{rentId}")]
        public async Task<MessageResponse> MarkCarTaken(int rentId)
        {
            return await _rentHistory.CarTaken(rentId);
        }






    }
}
