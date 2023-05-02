﻿using CarRental.Application.Common.Interface;
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
        [Route("/api/admin/quote-price")]
        public async Task<MessageResponse> QuoteCost(QuoteCostDTO quoteCost)
        {
            await _authService.GetSessionUser(new List<string> { "Admin", "Staff" });
            return await _damageRequest.QuoteCost(quoteCost);
        }
        
        [HttpPost]
        [Route("/api/admin/mark-paid/{damageID}")]
        public async Task<MessageResponse> MarkPaid(int id)
        {
            await _authService.GetSessionUser(new List<string> { "Admin", "Staff" });
            return await _damageRequest.MarkPaid(id);
        }

        public async Task<DamageRequestListDTO> GetDamageRequests()
        {
            await _authService.GetSessionUser(new List<string> { "Admin", "Staff" });
            
            return await _damageRequest.GetAllRequests();
        }




    }
}
