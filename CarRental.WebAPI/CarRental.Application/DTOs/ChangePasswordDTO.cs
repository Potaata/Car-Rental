using CarRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Application.DTOs
{
    public class ChangePasswordDTO
    {
        public string currentPassword { get; set; }
        public string newPassword { get; set; }

    }
}
