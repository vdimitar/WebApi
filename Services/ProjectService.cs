using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.DTOs;
using WebApi.IServices;
using WebApi.Models;

namespace WebApi.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IBaseRepository<Project> _repository;
        private readonly IRandomStringGeneratorService _randomStringService;
        private readonly ApplicationDbContext _context;

        public ProjectService
            (
            IBaseRepository<Project> repository,
            IRandomStringGeneratorService randomStringService,
            ApplicationDbContext context
            )
        {
            _repository = repository;
            _randomStringService = randomStringService;
            _context = context;
        }

        public async Task<IEnumerable<ProjectDTO>> GetAllProjects()
        {
            var projects = await _repository.GetAllAsync();

            return projects.Select(p => new ProjectDTO
            {
                Id = p.Id,
                Name = p.Name,
                Budget = p.Budget,
                ProjectCode = p.ProjectCode
            });
        }

        public async Task<ProjectDTO> GetProjectById(int id)
        {
            var project = await _repository.GetByIdAsync(id);
            if (project == null) return null;

            return new ProjectDTO
            {
                Id = project.Id,
                Name = project.Name,
                Budget = project.Budget,
                ProjectCode = project.ProjectCode
            };
        }

        public async Task<ProjectDTO> CreateProject(ProjectDTO projectDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            { 

                if (string.IsNullOrWhiteSpace(projectDto.Name) || projectDto.Budget <= 0)
                {
                    throw new ArgumentException("Project name and budget must be valid");
                }
                var project = new Project 
                {
                    Name = projectDto.Name,
                    Budget = projectDto.Budget,
                    ProjectCode = null
                };

                var createdProject = await _repository.AddAsync(project);
                await _context.SaveChangesAsync();

                int projectId = createdProject.Id;

                string projectCode = await _randomStringService.GetRandomProjectCode();

                string finalProjectCode = $"{projectCode}-{projectId}";

                createdProject.ProjectCode = finalProjectCode;
                _context.Projects.Update(createdProject);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return new ProjectDTO
                {
                    Id = createdProject.Id,
                    Name = createdProject.Name,
                    Budget = createdProject.Budget,
                    ProjectCode = createdProject.ProjectCode
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Project creation failed. Transaction rolled back");

            }
        } 
    

        public async Task<bool> UpdateProject(int id, ProjectDTO projectDto)
        {
            var project = new Project
            {
                Id = projectDto.Id,
                Name = projectDto.Name,
                Budget = projectDto.Budget,
                ProjectCode = projectDto.ProjectCode
            };

            return await _repository.UpdateAsync(id, project);
        }

        public async Task<bool> DeleteProject(int id)
        {
            return await _repository.SoftDeleteAsync(id);
        }
    }
}
