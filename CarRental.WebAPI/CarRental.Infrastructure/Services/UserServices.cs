using CarRental.Application.Common.Interface;
using CarRental.Application.DTOs;
using CarRental.Domain.Entities;
using CarRental.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Nanoid;
using CarRental.Application.DTOs.CarDTOs;
using System.Security.Cryptography.Xml;

namespace CarRental.Infrastructure.Services
{
    public class UserServices : IUsers
    {
        private readonly IApplicationDBContext _dbcontext;
        private readonly UserManager<Users> _userManager;

        public UserServices(IApplicationDBContext dbContext, UserManager<Users> userManager)
        {
            _dbcontext = dbContext;
            _userManager = userManager;
        }
        public async Task<MessageResponse> RegisterUser(UserRegisterRequestDTO users)
        {
            var DBUser = new Users { UserName = users.Username, Email = users.Email, Address = users.Address, Name = users.Name};
            var result = await _userManager.CreateAsync(DBUser, users.RawPassword);
            if (!result.Succeeded)
            {
                string error = result.Errors.ElementAt(0).Description;
                throw new ApiException(error);
            }
            
            await _userManager.AddToRoleAsync(DBUser, "User");
            return new MessageResponse { message = "Registration succeessful!" };
        }

        public async Task<UserLoginResponseDTO> LoginUser(UserLoginRequestDTO credentials)
        {
            var DBUser = await _userManager.FindByEmailAsync(credentials.Email);

            if (DBUser == null)
            {
                string error = "Invalid Email or Password!";
                throw new ApiException(error);
            }

            bool signInResult = await _userManager.CheckPasswordAsync(DBUser, credentials.RawPassword);
            if (!signInResult)
            {
                string error = "Invalid Email or Password!";
                throw new ApiException(error);
            }

            string token = Nanoid.Nanoid.Generate(size: 21);
            var setTokenResult = await _userManager.SetAuthenticationTokenAsync(DBUser, "Default", "Login", token);
            if (!setTokenResult.Succeeded)
            {
                string error = "Could not complete request!";
                throw new ApiException(error);
            }

            string userRole = (await _userManager.GetRolesAsync(DBUser)).First();

            UserLoginResponseDTO resp = new UserLoginResponseDTO
            {
                Username = DBUser.UserName,
                Role = userRole,
                Token = token
            };

            return resp;
        }
    }
}
