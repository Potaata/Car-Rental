using CarRental.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Application.Common.Interface
{
    public interface IUsers
    {
        public Task<UserRegisterRequestDTO> AddUsers(UserRegisterRequestDTO user);



    }
}
