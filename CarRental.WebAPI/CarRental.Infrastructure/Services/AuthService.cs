using CarRental.Application.Common.Interface;
using CarRental.Domain.Entities;
using CarRental.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Application.DTOs;

namespace CarRental.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpContext _httpContext;
        private readonly UserManager<Users> _userManager;
        private readonly IApplicationDBContext _dbContext;

        public AuthService(IApplicationDBContext dBContext, IHttpContextAccessor httpContextAccessor, UserManager<Users> userManager)
        {
            _dbContext = dBContext;
            _httpContext = httpContextAccessor.HttpContext;
            _userManager = userManager;
        }
        public async Task<UserLoginResponseDTO> ValidateToken(string token)
        {

            if (string.IsNullOrEmpty(token))
                throw new ApiException("Invalid Token!", System.Net.HttpStatusCode.Unauthorized);

            // the token is in table: aspnetusertokens
            // join with aspnetusers to get user details
            // if not found throw exception
            // if found create a IdntityUsr and return
            var user = await _dbContext.Users
                                .FromSqlInterpolated($@"
                                    SELECT anu.* 
                                    FROM ""AspNetUserTokens"" anut 
                                    JOIN ""AspNetUsers"" anu 
                                    ON anut.""UserId"" = anu.""Id"" 
                                    WHERE anut.""Value"" = {token}
                                    ").FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ApiException("Invalid Token!", System.Net.HttpStatusCode.Unauthorized);
            }

            // TODO: get role and validate that as well
            return new UserLoginResponseDTO
            {
                Token = token,
                Username = user.UserName,
                Role = "User"
            };
        }

        public async Task<Users> GetSessionUser()
        {
            string token = _httpContext.Request.Headers.Authorization;

            if (string.IsNullOrEmpty(token))
                throw new ApiException("Invalid Token!", System.Net.HttpStatusCode.Unauthorized);

            // the token is in table: aspnetusertokens
            // join with aspnetusers to get user details
            // if not found throw exception
            // if found create a IdntityUsr and return
            var user = await _dbContext.Users
                                .FromSqlInterpolated($@"
                                    SELECT anu.* 
                                    FROM ""AspNetUserTokens"" anut 
                                    JOIN ""AspNetUsers"" anu 
                                    ON anut.""UserId"" = anu.""Id"" 
                                    WHERE anut.""Value"" = {token}
                                    ").FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ApiException("Invalid Token!", System.Net.HttpStatusCode.Unauthorized);
            }
            return user;
        }
    }
}
