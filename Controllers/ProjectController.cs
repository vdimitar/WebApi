using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.IServices;
using WebApi.Models;
using WebApi.DTOs;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects()
        {
            var projects = await _projectService.GetAllProjects();


            var projectDtos = projects.Select(p => new ProjectDTO
            {
                Id = p.Id,
                Name = p.Name,
                Budget = p.Budget,
                ProjectCode = p.ProjectCode
            });

            return Ok(projectDtos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> GetProject(int id)
        {
            var project = await _projectService.GetProjectById(id);
            if (project == null)
                return NotFound();


            var projectDto = new ProjectDTO
            {
                Id = project.Id,
                Name = project.Name,
                Budget = project.Budget,
                ProjectCode = project.ProjectCode
            };

            return Ok(projectDto);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> CreateProject([FromBody] ProjectDTO projectDto)
        {
            var createdProject = await _projectService.CreateProject(projectDto);
            return CreatedAtAction(nameof(GetProject), new { id = createdProject.Id }, createdProject);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectDTO projectDto)
        {
            if (id != projectDto.Id)
                return BadRequest("Mismatched project ID");

            var updated = await _projectService.UpdateProject(id, projectDto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var deleted = await _projectService.DeleteProject(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
