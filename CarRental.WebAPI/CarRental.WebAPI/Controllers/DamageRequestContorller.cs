using CarRental.Application.Common.Interface;
using CarRental.Application.DTOs;
using CarRental.Application.DTOs.CarDTOs;
using CarRental.Application.DTOs.RentHistoryDTOs;
using CarRental.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebAPI.Controllers
{
    [ApiController]
    public class DamageRequestController : ControllerBase
    {
        private readonly IDamageRequest _damageRequest;
        private readonly IApplicationDBContext _dbcontext;
        private readonly IAuthService _authService;


        public DamageRequestController(IDamageRequest damageRequest, IApplicationDBContext dbcontext, IAuthService authService)
        {
            _damageRequest = damageRequest;
            _dbcontext = dbcontext;
            _authService = authService;
        }

        [HttpPost]
        [Route("/api/users/damage-request")]
        public async Task<MessageResponse> AddDamageRequest(DamageRequest newDamageRequest)
        {
            await _authService.GetSessionUser(new List<string> { "Admin", "Staff", "User" });
            return await _damageRequest.AddDamageRequest(newDamageRequest);
        }

        [HttpPost]
        [Route("/api/admin/quote-price/{damageId}")]
        public async Task<MessageResponse> QuoteCost(int damageId, QuoteCostDTO quoteCost)
        {
            await _authService.GetSessionUser(new List<string> { "Admin", "Staff" });
            return await _damageRequest.QuoteCost(damageId, quoteCost.Cost);
        }
        
        [HttpPost]
        [Route("/api/admin/mark-paid/{damageID}")]
        public async Task<MessageResponse> MarkPaid(int damageId)
        {
            await _authService.GetSessionUser(new List<string> { "Admin", "Staff" });
            return await _damageRequest.MarkPaid(damageId);
        }

        [HttpGet]
        [Route("/api/admin/damage-requests")]
        public async Task<DamageRequestListResponseDTO> GetDamageRequests()
        {
            await _authService.GetSessionUser(new List<string> { "Admin", "Staff" });
            
            return await _damageRequest.GetAllRequests();
        }
    }
}
