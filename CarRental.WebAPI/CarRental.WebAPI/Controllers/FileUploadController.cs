using CarRental.Application.Common.Interface;
using CarRental.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUpload _fileUploadService;

        public FileUploadController(IFileUpload fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [HttpPost]
        public async Task<UrlResponse> UploadFile(IFormFile file)
        {
           
            var url = await _fileUploadService.UploadFileAsync(file);

            return url;
        }
    }
}
