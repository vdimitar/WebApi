using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.DTOs;

namespace WebApi.IServices
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDTO>> GetAllProjects();
        Task<ProjectDTO> GetProjectById(int id);
        Task<ProjectDTO> CreateProject(ProjectDTO projectDto);
        Task<bool> UpdateProject(int id, ProjectDTO projectDto);
        Task<bool> DeleteProject(int id);
    }
}
