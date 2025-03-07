using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.IServices;
using WebApi.Models;

namespace WebApi.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IBaseRepository<Project> _repository;

        public ProjectService
            (
            IBaseRepository<Project> repository
            )
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Project> GetProjectById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Project> CreateProject(Project project)
        {
            return await _repository.AddAsync(project);
        }

        public async Task<bool> UpdateProject(int id, Project project)
        {
            return await _repository.UpdateAsync(id, project);
        }

        public async Task<bool> DeleteProject(int id)
        {
            return await _repository.SoftDeleteAsync(id);
        }
    }
}
