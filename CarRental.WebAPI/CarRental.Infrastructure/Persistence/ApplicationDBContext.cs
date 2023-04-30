using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Application.Common.Interface;
using CarRental.Domain.Entities;
using CarRental.Domain.Shared;

namespace CarRental.Infrastructure.Persistence
{
    public class ApplicationDBContext : IdentityDbContext<Users, IdentityRole, string>, IApplicationDBContext
    {
        private readonly IDateTime _dateTime;

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options, IDateTime dateTime) : base(options)
        {
            _dateTime = dateTime;
        }

        public DbSet<Cars> Cars { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<RentHistory> RentHistory { get; set; }
        public DbSet<Users> Users { get; set; }
        
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "Staff", NormalizedName = "STAFF" },
                new IdentityRole { Id = "3", Name = "User", NormalizedName = "USER" }
            );
        }
    }
}
