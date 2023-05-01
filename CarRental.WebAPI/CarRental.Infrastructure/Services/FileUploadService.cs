using CarRental.Application.Common.Interface;
using CarRental.Application.DTOs;
using CarRental.Application.DTOs.CarDTOs;
using CarRental.Infrastructure.Exceptions;
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
        public async Task<UrlResponse> UploadFileAsync(IFormFile file)
        {
            // create random file name

            if (file.Length > 1572864)
            {
                throw new ApiException("The file size is greater than 1.5 MB.");
            }

            var randomFileName = $"{Path.GetRandomFileName()}{Path.GetRandomFileName()}";

            var fileExtension = Path.GetExtension(file.FileName);

            var fileName = $"{randomFileName}{fileExtension}";

            var filePath = "../../static/" + fileName;

            await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            await file.CopyToAsync(fileStream);


            return new UrlResponse { Url = "http://localhost:8000/" + fileName };
        }
    }
}

