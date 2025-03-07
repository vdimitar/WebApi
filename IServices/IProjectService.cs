using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.IServices
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetProjectById(int id);
        Task<Project> CreateProject(Project project);
        Task<bool> UpdateProject(int id, Project project);
        Task<bool> DeleteProject(int id);
    }
}
