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
        private readonly IAuthService _authService;

        public FileUploadController(IFileUpload fileUploadService, IAuthService authService)
        {
            _authService = authService;
            _fileUploadService = fileUploadService;
        }

        [HttpPost]
        public async Task<UrlResponse> UploadFile(IFormFile file)
        {
            await _authService.GetSessionUser(new List<string> { "Admin", "Staff", "User" });
            var url = await _fileUploadService.UploadFileAsync(file);
            return url;
        }
    }
}
