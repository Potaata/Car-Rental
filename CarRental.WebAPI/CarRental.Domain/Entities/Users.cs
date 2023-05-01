using CarRental.Domain.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Domain.Entities
{
    
    public class Users : IdentityUser
    {
        public string Name {get; set;}
        public string Address {get; set;}
        public string? Documenturl { get; set; }
    }
}
