using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.IServices;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            var employees = await _employeeService.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> CreateEmployee([FromBody] EmployeeDTO employeeDto)
        {
            if (employeeDto == null)
                return BadRequest("Invalid employee data");

            var createdEmployee = await _employeeService.CreateEmployee(employeeDto);
            return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.Id }, createdEmployee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDTO employeeDto)
        {
            if (employeeDto == null || id != employeeDto.Id)
                return BadRequest("Invalid employee data");

            var updated = await _employeeService.UpdateEmployee(id, employeeDto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var deleted = await _employeeService.DeleteEmployee(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
