﻿using CarRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Application.DTOs
{
    public class DamageRequestListDTO
    {
        public List<DamageRequest> damageRequests { get; set; }
    }
}
