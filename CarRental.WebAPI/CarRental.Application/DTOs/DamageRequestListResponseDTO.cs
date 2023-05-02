using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Application.DTOs
{
    public class DamageRequestListResponseDTO
    {
        public List<DamageRequestJoinedDTO>? damageRequests { get; set; }
    }
}
