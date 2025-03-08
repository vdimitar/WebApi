using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.IServices;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetDepartments()
        {
            var departments = await _departmentService.GetAllDepartments();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDTO>> GetDepartment(int id)
        {
            var department = await _departmentService.GetDepartmentById(id);
            if (department == null)
                return NotFound();

            return Ok(department);
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentDTO>> CreateDepartment([FromBody] DepartmentDTO departmentDto)
        {
            if (departmentDto == null)
                return BadRequest("Invalid department data");

            var createdDepartment = await _departmentService.CreateDepartment(departmentDto);
            return CreatedAtAction(nameof(GetDepartment), new { id = createdDepartment.Id }, createdDepartment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDTO departmentDto)
        {
            if (departmentDto == null || id != departmentDto.Id)
                return BadRequest("Invalid department data");

            var updated = await _departmentService.UpdateDepartment(id, departmentDto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var deleted = await _departmentService.DeleteDepartment(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }


        [HttpGet("{id}/totalBudget")]
        public async Task<IActionResult> GetTotalBudgetByDepartment(int id)
        {
            var totalBudget = await _departmentService.GetTotalBudgetByDepartmentId(id);
            return Ok(new { departmentId = id, totalBudget });
        }
    }
}
