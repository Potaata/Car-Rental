using CarRental.Application.DTOs;
using CarRental.Application.DTOs.CarDTOs;
using CarRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Application.Common.Interface
{
    public interface IDamageRequest
    {
        public Task<MessageResponse> AddDamageRequest(DamageRequest damageRequest);

        public Task<MessageResponse> QuoteCost(int damageId, float cost);

        public Task<MessageResponse> MarkPaid(int id);
        
        public Task<DamageRequestListResponseDTO> GetAllRequests();

    }
}
