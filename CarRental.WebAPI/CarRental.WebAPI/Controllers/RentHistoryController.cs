using CarRental.Application.Common.Interface;
using CarRental.Application.DTOs.CarDTOs;
using CarRental.Application.DTOs.RentHistoryDTOs;
using CarRental.Domain.Entities;
using CarRental.Domain.Enums;
using CarRental.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRental.Application.DTOs;

namespace CarRental.WebAPI.Controllers
{
    [ApiController]
    public class RentHistoryController : ControllerBase
    {
        private readonly IRentHistory _rentHistory;
        private readonly IApplicationDBContext _dbcontext;
        private readonly IAuthService _authService;
        private readonly IDateTime _dateTime;

        public RentHistoryController(IRentHistory rentHistory, IApplicationDBContext dbcontext, IAuthService authService, IDateTime dateTime)
        {
            _rentHistory = rentHistory;
            _dbcontext = dbcontext;
            _authService = authService;
            _dateTime = dateTime;
        }

        [HttpGet]
        [Route("/api/admin/renthistory")]
        public async Task<RentHistoryListResponseDTO> GetRentHistories()
        {
            await _authService.GetSessionUser(new List<string> { "Admin", "Staff" });
            var allHistories = await _rentHistory.GetRentHistories();
            var response = new RentHistoryListResponseDTO { rents = allHistories };
            return response;
        }

        [HttpPost]
        [Route("api/user/add-rent")]
        public async Task<MessageResponse> AddRentHistory(RentHistory newRent)
        {
            Users user = await _authService.GetSessionUser(new List<string> { "User", "Admin", "Staff" });
            if (string.IsNullOrEmpty(user.Documenturl))
            {
                throw new ApiException("Please Upload your documents to Rent a car.");
            }
            newRent.UserId = user.Id;
            return await _rentHistory.AddRentHistory(newRent);
        }

        [HttpPost]
        [Route("api/admin/approve-request/{rentId}")]
        public async Task<MessageResponse> ApproveRequest(int rentId)
        {
            _dateTime.ValidateDateTime();
            await _authService.GetSessionUser(new List<string> { "Admin", "Staff" });
            return await _rentHistory.ApproveRequest(rentId);
        }

        [HttpPost]
        [Route("api/admin/deny-request/{rentId}")]
        public async Task<MessageResponse> DenyRequest(int rentId)
        {
            _dateTime.ValidateDateTime();

            await _authService.GetSessionUser(new List<string> { "Admin", "Staff" });
            return await _rentHistory.DeclineRequest(rentId);
        }

        [HttpGet]
        [Route("api/user/rent-history")]
        public async Task<UserRentHistoryListResponse> GetRentHistoriesByUserId()
        {
            Users user = await _authService.GetSessionUser(new List<string> { "User", "Admin", "Staff" });
            return new UserRentHistoryListResponse { rents = await _rentHistory.GetRentHistoriesByUserId(user.Id) };
        }

        [HttpPost]
        [Route("api/admin/car-taken/{rentId}")]
        public async Task<MessageResponse> MarkCarTaken(int rentId)
        {
            _dateTime.ValidateDateTime();

            await _authService.GetSessionUser(new List<string> { "Admin", "Staff" });
            return await _rentHistory.CarTaken(rentId);
        }

        [HttpGet]
        [Route("/api/users/discount")]
        public async Task<DiscountOfferResponse> GetDiscounts()
        {
            await _authService.GetSessionUser(new List<string> { "Admin", "Staff", "User" });
            return new DiscountOfferResponse { offer = await _rentHistory.GetValidDiscount() };
        }

        [HttpPost]
        [Route("/api/admin/add-offer")]
        public async Task<MessageResponse> CreateDiscountOffer(DiscountOfferResponse offer)
        {
            _dateTime.ValidateDateTime();

            await _authService.GetSessionUser(new List<string> { "Admin", "Staff" });
            Offers o = await _rentHistory.GetValidDiscount();
            if (o != null)
            {
                throw new ApiException("A discount is already on offers.");
            }
            offer.offer.ValidTill = offer.offer.ValidTill.ToUniversalTime();
            await _rentHistory.AddOffer(offer.offer);
            return new MessageResponse { message = "Offer added successfullly!!" };
        }

        [HttpPost]
        [Route("api/admin/car-returned/{rentId}")]
        public async Task<MessageResponse> CarReturned(int rentId)
        {
            _dateTime.ValidateDateTime();

            await _authService.GetSessionUser(new List<string> { "Admin", "Staff" });
            return await _rentHistory.CarReturned(rentId);
        }


    }
}
