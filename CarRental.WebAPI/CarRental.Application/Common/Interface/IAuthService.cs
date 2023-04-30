using CarRental.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Application.DTOs;

namespace CarRental.Application.Common.Interface
{
    public interface IAuthService
    {
        public Task<UserLoginResponseDTO> ValidateToken(string token);
        public Task<Users> GetSessionUser();
    }
}
