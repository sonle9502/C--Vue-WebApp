using HealthcareApp.Api.Models.Entities;
using HealthcareApp.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _service;

        public PatientsController(IPatientService service)
        {
            _service = service;
        }

        [HttpGet]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetAll()
        {
            var hospitalIdClaim = User.FindFirst("HospitalId");

            if (hospitalIdClaim == null || !int.TryParse(hospitalIdClaim.Value, out var hospitalId))
            {
                return Unauthorized("HospitalId claim is missing or invalid. Please login again.");
            }

            var patients = await _service.GetAllPatients(hospitalId);
            return Ok(patients);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> Get(int id)
        {
            var patient = await _service.GetPatient(id);
            if (patient == null) return NotFound();
            return Ok(patient);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> Create(Patient patient)
        {
            // Lấy HospitalId từ claim
            var hospitalIdClaim = User.Claims.FirstOrDefault(c => c.Type == "HospitalId")?.Value;
            if (hospitalIdClaim == null) return Forbid();
            patient.HospitalId = int.Parse(hospitalIdClaim);

            await _service.RegisterPatient(patient);
            return CreatedAtAction(nameof(Get), new { id = patient.Id }, patient);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Doctor,User")]
        public async Task<ActionResult<Patient>> Update(int id, Patient updatedPatient)
        {
            // IDが一致してるか確認
            if (id != updatedPatient.Id)
            {
                return BadRequest("IDが一致している.");
            }

            var existingPatient = await _service.GetPatient(id);
            if (existingPatient == null)
            {
                return NotFound();
            }

            // Gọi service để cập nhật thông tin
            await _service.UpdatePatient(updatedPatient);

            return Ok(updatedPatient);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Doctor,User")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingPatient = await _service.GetPatient(id);
            if (existingPatient == null)
            {
                return NotFound();
            }

            await _service.DeletePatient(existingPatient);
            return NoContent(); // 204, không cần trả dữ liệu
        }

    }
}
