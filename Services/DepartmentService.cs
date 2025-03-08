using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.DTOs;
using WebApi.IServices;
using WebApi.Models;

namespace WebApi.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IBaseRepository<Department> _repository;
        private readonly ApplicationDbContext _context;

        public DepartmentService
            (
            IBaseRepository<Department> repository,
            ApplicationDbContext context
            )
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<DepartmentDTO>> GetAllDepartments()
        {
            var departments = await _repository.GetAllAsync();
            return departments.Select(d => new DepartmentDTO
            {
                Id = d.Id,
                Name = d.Name,
                OfficeLocation = d.OfficeLocation
            });
        }

        public async Task<DepartmentDTO> GetDepartmentById(int id)
        {
            var department = await _repository.GetByIdAsync(id);
            if (department == null) return null;

            return new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name,
                OfficeLocation = department.OfficeLocation
            };
        }

        public async Task<DepartmentDTO> CreateDepartment(DepartmentDTO departmentDto)
        {
            var department = new Department
            {
                Name = departmentDto.Name,
                OfficeLocation = departmentDto.OfficeLocation
            };

            var createdDepartment = await _repository.AddAsync(department);

            return new DepartmentDTO
            {
                Id = createdDepartment.Id,
                Name = createdDepartment.Name,
                OfficeLocation = createdDepartment.OfficeLocation
            };
        }

        public async Task<bool> UpdateDepartment(int id, DepartmentDTO departmentDto)
        {
            var department = await _repository.GetByIdAsync(id);
            if (department == null) return false;

            department.Name = departmentDto.Name;
            department.OfficeLocation = departmentDto.OfficeLocation;

            return await _repository.UpdateAsync(id, department);
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            return await _repository.SoftDeleteAsync(id);
        }

        public async Task<decimal>GetTotalBudgetByDepartmentId(int departmentId)
        {
            var totalBudget = await _context.Employees
                .Where(e => e.DepartmentId == departmentId)
                .SelectMany(e => e.EmployeeProjects)
                .Select(ep => ep.Project.Budget)
                .Distinct()
                .SumAsync();

            return totalBudget;
        }
    }
}
