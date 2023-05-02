using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Domain.Entities;

namespace CarRental.Application.DTOs
{
    public class UserWithRoleDTO: Users
    {
        public string Role { get; set; }
    }
}
