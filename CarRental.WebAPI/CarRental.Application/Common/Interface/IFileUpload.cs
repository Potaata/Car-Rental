using CarRental.Application.DTOs.CarDTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Application.Common.Interface
{
    public interface IFileUpload
    {
        Task<MessageResponse> UploadFileAsync(IFormFile file);
    }
}
