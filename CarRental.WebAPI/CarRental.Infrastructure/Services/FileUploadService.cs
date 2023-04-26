using CarRental.Application.Common.Interface;
using CarRental.Application.DTOs.CarDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Infrastructure.Services
{
    public class FileUploadService : IFileUpload
    {
        public async Task<MessageResponse> UploadFileAsync(IFormFile file)
        {
            // create random file name


            var randomFileName = $"{Path.GetRandomFileName()}{Path.GetRandomFileName()}";

            var fileExtension = Path.GetExtension(file.FileName);

            var fileName = $"{randomFileName}{fileExtension}";

            var filePath = "../../static/" + fileName;

            await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            await file.CopyToAsync(fileStream);


            return new MessageResponse { message = "File added successfully." };
        }
    }
}

