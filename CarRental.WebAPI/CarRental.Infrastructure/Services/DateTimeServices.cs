using CarRental.Application.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Infrastructure.Services
{
    public class DateTimeServices : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
