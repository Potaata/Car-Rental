using CarRental.Application.Common.Interface;
using CarRental.Application.DTOs;
using CarRental.Application.DTOs.CarDTOs;
using CarRental.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _dbcontext.DamageRequest.Add(damageRequest);

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
    }
}
