using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.DTOs;
using WebApi.Models;

namespace WebApi.IServices
{
    public interface IEmployeeProjectService
    {
        Task<IEnumerable<EmployeeProjectDTO>> GetAllEmployeeProjects();
        Task<IEnumerable<EmployeeProjectDTO>> GetEmployeeProjects(int employeeId);
        Task<bool> AssignEmployeeToProject(int employeeId, int projectId, string role);
        Task<bool> RemoveEmployeeFromProject(int employeeId, int projectId);
    }
}
