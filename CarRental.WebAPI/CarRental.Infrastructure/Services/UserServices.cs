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
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Services
{
    public class UserServices : IUsers
    {
        private readonly IApplicationDBContext _dbcontext;
        private readonly UserManager<Users> _userManager;
        private readonly IAuthService _authService;

        public UserServices(IApplicationDBContext dbContext, UserManager<Users> userManager, IAuthService authService)
        {
            _dbcontext = dbContext;
            _userManager = userManager;
            _authService = authService;
        }
        public async Task<MessageResponse> RegisterUser(UserRegisterRequestDTO users)
        {
            var DBUser = new Users { UserName = users.Username, Email = users.Email, Address = users.Address, Name = users.Name };
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

        public async Task<UserListResponseDTO> GetRegularUsers()
        {
            DateTime oneMonthAgo = DateTime.Now.ToUniversalTime().AddDays(-30);
            var regularUsers = from u in _dbcontext.Users
                               join rh in _dbcontext.RentHistory
                               on u.Id equals rh.UserId
                               where rh.ToDate >= oneMonthAgo
                               select new Users
                               {
                                   Address = u.Address,
                                   Name = u.Name,
                                   PhoneNumber = u.PhoneNumber,
                                   Email = u.Email
                               };
            return new UserListResponseDTO { users = (await regularUsers.ToListAsync()) };
        }

        public async Task<UserListResponseDTO> GetAllUsers()
        {
            var regularUsers = from u in _dbcontext.Users
                               select new Users
                               {
                                   Id = u.Id,
                                   Address = u.Address,
                                   Name = u.Name,
                                   PhoneNumber = u.PhoneNumber,
                                   Email = u.Email
                               };
            return new UserListResponseDTO { users = (await regularUsers.ToListAsync()) };
        }

        public async Task<UserListResponseDTO> GetInactiveUsers()
        {
            DateTime threeMonthsAgo = DateTime.Now.ToUniversalTime().AddDays(-90);

            var threeeMonthsRentingUsersDB = from u in _dbcontext.Users
                                             join rh in _dbcontext.RentHistory
                                             on u.Id equals rh.UserId
                                             where rh.ToDate >= threeMonthsAgo
                                             select new Users
                                             {
                                                 Id = u.Id,
                                                 Address = u.Address,
                                                 Name = u.Name,
                                                 PhoneNumber = u.PhoneNumber,
                                                 Email = u.Email
                                             };

            UserListResponseDTO allUsers = await GetAllUsers();

            var threeeMonthsRentingUsers = await threeeMonthsRentingUsersDB.ToListAsync();
            var inactiveUsers = allUsers.users
                                .Where(user => !threeeMonthsRentingUsers.Any(activeUser => activeUser.Id == user.Id))
                                .ToList();

            return new UserListResponseDTO { users = inactiveUsers };
        }

        public async Task<MessageResponse> ChangePassword(ChangePasswordDTO passwords)
        {
            var result = await _userManager.ChangePasswordAsync((await _authService.GetSessionUser(new List<string> { "User", "Staff", "Admin" })), passwords.currentPassword, passwords.newPassword);
            if (!result.Succeeded)
            {
                string error = result.Errors.ElementAt(0).Description;
                throw new ApiException(error);
            }

            return new MessageResponse { message = "Password Changed Succeessfully!" };

        }

        public async Task<MessageResponse> UpdatePhotoUrl(string userId, string photoUrl)
        {
            var user = await _dbcontext.Users.FindAsync(userId);
            user.Documenturl = photoUrl;
            await _dbcontext.SaveChangesAsync();
            return new MessageResponse { message = "Photo Updated Successfully!" };
        }

        public async Task<List<UserWithRoleDTO>> GetAllStaffs() 
        {
            var allUsers = await GetAllUsers();

            var staffs = new List<UserWithRoleDTO>();
            foreach (var user in allUsers.users)
            {
                if (await _userManager.IsInRoleAsync(user, "Staff") || await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    user.Role = (await _userManager.GetRolesAsync(user)).First();
                    staffs.Add(user);
                }
            }
            return staffs;
        }
        public async Task<MessageResponse> AddStaff(UserRegisterRequestDTO users) 
        {
            var DBUser = new Users { UserName = users.Username, Email = users.Email, Address = users.Address, Name = users.Name };
            var result = await _userManager.CreateAsync(DBUser, users.RawPassword);
            if (!result.Succeeded)
            {
                string error = result.Errors.ElementAt(0).Description;
                throw new ApiException(error);
            }

            await _userManager.AddToRoleAsync(DBUser, "Staff");
            return new MessageResponse { message = "Registration succeessful!" };
        }
        
        public async Task<MessageResponse> AddAdmin(UserRegisterRequestDTO users)
        {
            var DBUser = new Users { UserName = users.Username, Email = users.Email, Address = users.Address, Name = users.Name };
            var result = await _userManager.CreateAsync(DBUser, users.RawPassword);
            if (!result.Succeeded)
            {
                string error = result.Errors.ElementAt(0).Description;
                throw new ApiException(error);
            }

            await _userManager.AddToRoleAsync(DBUser, "Admin");
            return new MessageResponse { message = "Registration succeessful!" };
        }

    }
}
