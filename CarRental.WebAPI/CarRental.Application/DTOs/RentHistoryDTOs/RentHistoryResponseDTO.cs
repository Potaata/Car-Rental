using CarRental.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Application.DTOs.RentHistoryDTOs
{
    public class RentHistoryResponseDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? ApprovedBy { get; set; }
        public string CarModel { get; set; }
        public string CarNumberPlate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public StatusEnums Status;
        public int Price { get; set; }
    }
}
