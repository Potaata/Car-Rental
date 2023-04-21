using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Application.DTOs
{
    public class UserRegisterRequestDTO
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is Required. ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required. ")]
        public string RawPassword { get; set; }

        [Required(ErrorMessage = "Username is Required. ")]
        public string Username { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
