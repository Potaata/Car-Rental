using CarRental.Application.Common.Interface;
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

        public async Task<MessageResponse> QuoteCost(QuoteCostDTO quoteCost)
        {
            var damageRequest = await _dbcontext.DamageRequest.FindAsync(quoteCost.Id);

            damageRequest.Cost = quoteCost.Cost;

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

        public async Task<DamageRequestListDTO> GetAllRequests() {

            var requests = await _dbcontext.DamageRequest.ToListAsync();

            return new DamageRequestListDTO { damageRequests = requests };

        }
    }
}
