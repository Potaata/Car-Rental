using CarRental.Application.Common.Interface;
using CarRental.Application.DTOs;
using CarRental.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CarRental.Infrastructure.Services
{
    public class UserServices : IUsers
    {
        private readonly IApplicationDBContext _dbcontext;
        private readonly UserManager<IdentityUser> _userManager;

        public UserServices(IApplicationDBContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbcontext = dbContext;
            _userManager = userManager;
        }
        public async Task<UserRegisterRequestDTO> AddUsers(UserRegisterRequestDTO users)
        {
            var DBUser = new IdentityUser { UserName = users.Username, Email = users.Email };
            var result = await _userManager.CreateAsync(DBUser, users.RawPassword);
            if (!result.Succeeded)
            {
                string error = result.Errors.ElementAt(0).Description;
                throw new ApiException(error);
            }
            throw new Exception("This is a test exception");
            return users;
        }
    }
}
