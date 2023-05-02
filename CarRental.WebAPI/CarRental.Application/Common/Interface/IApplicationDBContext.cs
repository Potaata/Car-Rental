using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Application.Common.Interface
{
    public interface IApplicationDBContext
    {
        DbSet<Cars> Cars { get; set; }
        Task<int> SaveChangesAsync();
        DbSet<Notification> Notification { get; set; }
        DbSet<RentHistory> RentHistory { get; set; }
        DbSet<Users> Users { get; set; }
        DbSet<DamageRequest> DamageRequest { get; set; }

        DbSet<Offers> Offers { get; set; }
    }
}
