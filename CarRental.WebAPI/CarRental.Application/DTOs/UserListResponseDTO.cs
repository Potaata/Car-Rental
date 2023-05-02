using CarRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Application.DTOs
{
    public class UserListResponseDTO
    {
        public List<UserWithRoleDTO> users { get; set; }
    }
}
