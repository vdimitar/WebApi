using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DTOs;
using WebApi.IServices;
using WebApi.Models;

namespace WebApi.Services
{
    public class EmployeeProjectService : IEmployeeProjectService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmployeeService _employeeService;
        private readonly IProjectService _projectService;

        public EmployeeProjectService
            (
            ApplicationDbContext context,
            IEmployeeService employeeService,
            IProjectService projectService
            )
        {
            _context = context;
            _employeeService = employeeService;
            _projectService = projectService;
        }


        public async Task<IEnumerable<EmployeeProjectDTO>> GetAllEmployeeProjects()
        {
            return await _context.EmployeeProjects
                .Include(ep => ep.Employee)
                .Include(ep => ep.Project)
                .Select(ep => new EmployeeProjectDTO
                {
                    EmployeeId = ep.Employee.Id,
                    EmployeeName = ep.Employee.FirstName + " " + ep.Employee.LastName,
                    ProjectId = ep.Project.Id,
                    ProjectName = ep.Project.Name,
                    Role = ep.Role
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeProjectDTO>> GetEmployeeProjects(int employeeId)
        {
            return await _context.EmployeeProjects
           .Where(ep => ep.EmployeeId == employeeId)
           .Include(ep => ep.Project)
           .Select(ep => new EmployeeProjectDTO
           {
               EmployeeId = employeeId,
               EmployeeName = ep.Employee.FirstName + " " + ep.Employee.LastName,
               ProjectId = ep.Project.Id,
               ProjectName = ep.Project.Name,
               Role = ep.Role
           })
           .ToListAsync();
        }

        public async Task<bool> AssignEmployeeToProject(int employeeId, int projectId, string role)
        {

            var employee = await _employeeService.GetEmployeeById(employeeId);
            var project = await _projectService.GetProjectById(projectId);

            if (employee == null || project == null)
                return false; 

            var existingAssignment = await _context.EmployeeProjects
                .FirstOrDefaultAsync(ep => ep.EmployeeId == employeeId && ep.ProjectId == projectId);

            if (existingAssignment != null)
                return false; 

            var assignment = new EmployeeProject
            {
                EmployeeId = employeeId,
                ProjectId = projectId,
                Role = role
            };

            await _context.EmployeeProjects.AddAsync(assignment);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> RemoveEmployeeFromProject(int employeeId, int projectId)
        {

            var employee = await _employeeService.GetEmployeeById(employeeId);
            var project = await _projectService.GetProjectById(projectId);

            if (employee == null || project == null)
                return false;


            var assignment = await _context.EmployeeProjects
                .FirstOrDefaultAsync(ep => ep.EmployeeId == employeeId && ep.ProjectId == projectId);

            if (assignment == null)
                return false; 

            _context.EmployeeProjects.Remove(assignment);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
