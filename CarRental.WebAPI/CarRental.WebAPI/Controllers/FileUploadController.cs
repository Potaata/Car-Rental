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
            //if (file == null || file.Length == 0)
            //    return BadRequest("No file selected.");

            //await using var fileStream = new FileStream(file.FileName, FileMode.Create, FileAccess.Write);
            //await file.CopyToAsync(fileStream);


            //using var memoryStream = new MemoryStream();
            //try
            //{
            //    await file.CopyToAsync(memoryStream);
            //}
            //catch (Exception ex)
            //{
            //    // Log or handle the exception
            //    return BadRequest("Error copying file to memory stream.");
            //}

            //var fileName = file.FileName;
            var url = await _fileUploadService.UploadFileAsync(file);

            return url;
        }
    }
}
