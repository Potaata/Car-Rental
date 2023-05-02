using CarRental.Domain.Entities;
using CarRental.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Application.DTOs.RentHistoryDTOs
{
    public class UserRentHistoryListResponse
    {
        public List<RentHistory> rents { get; set; }
    }
}
