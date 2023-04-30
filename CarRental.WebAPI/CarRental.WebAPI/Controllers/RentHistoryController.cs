using CarRental.Application.Common.Interface;
using CarRental.Application.DTOs.CarDTOs;
using CarRental.Application.DTOs.RentHistoryDTOs;
using CarRental.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental.WebAPI.Controllers
{
    [ApiController]
    public class RentHistoryController : ControllerBase
    {
        private readonly IRentHistory _rentHistory;

        public RentHistoryController(IRentHistory rentHistory)
        {
            _rentHistory = rentHistory;
        }
        
        [HttpGet]
        [Route("/api/admin/renthistory")]
        public async Task<List<RentHistoryResponseDTO>> GetRentHistories()
        {
            var rentHistories = await _rentHistory.GetRentHistories();
            return rentHistories;
        }

        [HttpPost]
        [Route("api/user/add-rent")]
        public async Task<MessageResponse> AddRentHistory([FromBody] RentHistory rentHistory)
        {

                RentHistory newRentHistory = new RentHistory
                {
                    UserId = rentHistory.UserId,
                    CarId = rentHistory.CarId,
                    FromDate = rentHistory.FromDate,
                    ToDate = rentHistory.ToDate,
                    Price = rentHistory.Price
                };

                var message =  await _rentHistory.AddRentHistory(newRentHistory);

                return message;
            
        }





    }
}
