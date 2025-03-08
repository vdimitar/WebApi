using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.IServices;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeProjectController : ControllerBase
    {
        private readonly IEmployeeProjectService _employeeProjectService;

        public EmployeeProjectController(IEmployeeProjectService employeeProjectService)
        {
            _employeeProjectService = employeeProjectService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeProjects()
        {
            var projects = await _employeeProjectService.GetAllEmployeeProjects();
            return Ok(projects); 
        }


        [HttpGet("{employeeId}/projects")]
        public async Task<IActionResult> GetEmployeeProjects(int employeeId)
        {
            var projects = await _employeeProjectService.GetEmployeeProjects(employeeId);
            if (projects == null || !projects.Any())
                return NotFound("No projects found for this employee.");

            return Ok(projects);  
        }

        [HttpPost("{employeeId}/assign/{projectId}")]
        public async Task<IActionResult> AssignEmployeeToProject(int employeeId, int projectId, [FromBody] AssignEmployeeToProjectDTO request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Role))
                return BadRequest("Invalid request. Role is required.");

            var assigned = await _employeeProjectService.AssignEmployeeToProject(employeeId, projectId, request.Role);

            if (!assigned)
                return BadRequest("Employee already assigned or Employee/Project not found.");

            return Ok("Employee assigned successfully.");
        }

        [HttpDelete("{employeeId}/remove/{projectId}")]
        public async Task<IActionResult> RemoveEmployeeFromProject(int employeeId, int projectId)
        {
            var removed = await _employeeProjectService.RemoveEmployeeFromProject(employeeId, projectId);
            if (!removed)
                return NotFound("Employee not assigned to this project.");

            return Ok("Employee removed successfully.");
        }
    }
}
