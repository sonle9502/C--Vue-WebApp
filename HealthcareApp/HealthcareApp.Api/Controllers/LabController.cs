using HealthcareApp.Api.Services.lab;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LabController : ControllerBase
    {
        private readonly ILabService _service;

        public LabController(ILabService service)
        {
            _service = service;
        }

        [HttpPost("upload-zip")]
        public async Task<IActionResult> UploadZip([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File không hợp lệ");

            await _service.ProcessZipAsync(file);
            return Ok("Xử lý thành công!");
        }

        [HttpGet("results")]
        public async Task<IActionResult> GetResults()
        {
            var results = await _service.GetLabResultsAsync();
            return Ok(results); // trả về array LabResult
        }
    }
}
