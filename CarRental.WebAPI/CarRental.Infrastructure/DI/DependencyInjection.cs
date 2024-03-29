﻿using CarRental.Application.Common.Interface;
using CarRental.Domain.Entities;
using CarRental.Infrastructure.Persistence;
using CarRental.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace CarRental.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("CarRentalDB"),
                b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)), ServiceLifetime.Transient);

            services.AddScoped<IApplicationDBContext>(provider => provider.GetService<ApplicationDBContext>());
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IDateTime, DateTimeServices>();

            services.AddScoped<IUsers, UserServices>();
            services.AddScoped<IAuthService, AuthService>();
            
            services.AddScoped<ICars, CarServices>();
            services.AddScoped<IFileUpload, FileUploadService>();
            services.AddScoped<IRentHistory, RentHistoryService>();
            services.AddScoped<IDamageRequest, DamageRequestService>();
            services.AddScoped<INotification, NotificationService>();

            return services;
        }
    }
}
