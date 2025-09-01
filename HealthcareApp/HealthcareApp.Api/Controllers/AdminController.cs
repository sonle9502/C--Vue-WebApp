using Microsoft.AspNetCore.Authorization;
using HealthcareApp.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HealthcareApp.Api.Models.Entities;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IHospitalService _hospitalService;

    public AdminController(IHospitalService hospitalService)
    {
        _hospitalService = hospitalService;
    }

    [HttpPost("hospitals")]
    public async Task<ActionResult> CreateHospital(Hospital hospital)
    {
        var created = await _hospitalService.CreateHospitalAsync(hospital);
        return CreatedAtAction(nameof(GetHospital), new { id = created.Id }, created);
    }

    [HttpGet("hospitals/{id}")]
    public async Task<ActionResult<Hospital>> GetHospital(int id)
    {
        var hospital = await _hospitalService.GetHospitalAsync(id);
        if (hospital == null) return NotFound();
        return Ok(hospital);
    }
    [HttpGet("hospitals")]
    public async Task<ActionResult<IEnumerable<Hospital>>> GetAllHospitals()
    {
        var hospitals = await _hospitalService.GetAllHospitalsAsync();
        return Ok(hospitals.Select(h => new {
            h.Id,
            h.Name,
            h.Address,
        }));

    }
}
