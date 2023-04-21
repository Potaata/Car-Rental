using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Application.DTOs
{
    public class UserLoginRequestDTO
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is Required. ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required. ")]
        public string RawPassword { get; set; }
    }
}
