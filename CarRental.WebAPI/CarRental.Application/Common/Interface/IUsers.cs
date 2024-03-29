﻿using CarRental.Application.DTOs;
using CarRental.Application.DTOs.CarDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Domain.Entities;

namespace CarRental.Application.Common.Interface
{
    public interface IUsers
    {
        public Task<MessageResponse> RegisterUser(UserRegisterRequestDTO user);
        public Task<UserLoginResponseDTO> LoginUser(UserLoginRequestDTO user);
        public Task<UserListResponseDTO> GetRegularUsers();
        public Task<UserListResponseDTO> GetAllUsers();
        public Task<UserListResponseDTO> GetInactiveUsers();
        public Task<MessageResponse> ChangePassword(ChangePasswordDTO passwords);
        public Task<MessageResponse> UpdatePhotoUrl(string userId, string photoUrl);
        public Task<List<UserWithRoleDTO>> GetAllStaffs();
        public Task<MessageResponse> AddStaff(UserRegisterRequestDTO user);
        public Task<MessageResponse> AddAdmin(UserRegisterRequestDTO user);
    }
}
