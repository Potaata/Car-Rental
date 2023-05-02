using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Application.DTOs
{
    public class DamageRequestJoinedDTO
    {
        public int? Id { get; set; }
        public string? CarModel { get; set; }
        public string? Description { get; set; }
        public bool? IsPaid { get; set; }
        public float? Cost { get; set; }
    }
}
