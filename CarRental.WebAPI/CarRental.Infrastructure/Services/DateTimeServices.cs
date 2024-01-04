using CarRental.Application.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Infrastructure.Exceptions;

namespace CarRental.Infrastructure.Services
{
    public class DateTimeServices : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;

        public void ValidateDateTime()
        {
            return;
            if (DateTime.Now.Hour >= 9 && DateTime.Now.Hour < 17)
            {
                return;
            }
            throw new ApiException("You are not allowed to perform this action at this time.");
        }
    }
}
