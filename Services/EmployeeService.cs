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
    public class EmployeeService : IEmployeeService
    {
        private readonly IBaseRepository<Employee> _repository;
        private readonly ApplicationDbContext _context;

        public EmployeeService(IBaseRepository<Employee> repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployees()
        {
            var employees = await _repository.GetAllAsync();

            return employees.Select(e => new EmployeeDTO
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Salary = e.Salary,
                DepartmentId = e.DepartmentId
            }).ToList();
        }

        public async Task<EmployeeDTO> GetEmployeeById(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.EmployeeProjects)
                    .ThenInclude(ep => ep.Project)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null) return null;

            return new EmployeeDTO
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId,
                Department = new DepartmentDTO
                {
                    Id = employee.Department.Id,
                    Name = employee.Department.Name,
                    OfficeLocation = employee.Department.OfficeLocation
                },
                EmployeeProjects = employee.EmployeeProjects?.Select(ep => new EmployeeProjectDTO
                {
                    EmployeeId = ep.EmployeeId,
                    ProjectId = ep.ProjectId,
                    Role = ep.Role,
                    Project = new ProjectDTO
                    {
                        Id = ep.Project.Id,
                        Name = ep.Project.Name,
                        Budget = ep.Project.Budget,
                        ProjectCode = ep.Project.ProjectCode
                    }
                }).ToList()
            };
        }

        public async Task<EmployeeDTO> CreateEmployee(EmployeeDTO employeeDto)
        {
            var employee = new Employee
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                Salary = employeeDto.Salary,
                DepartmentId = employeeDto.DepartmentId
            };

            var createdEmployee = await _repository.AddAsync(employee);

            return new EmployeeDTO
            {
                Id = createdEmployee.Id,
                FirstName = createdEmployee.FirstName,
                LastName = createdEmployee.LastName,
                Email = createdEmployee.Email,
                Salary = createdEmployee.Salary,
                DepartmentId = createdEmployee.DepartmentId
            };
        }

        public async Task<bool> UpdateEmployee(int id, EmployeeDTO employeeDto)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
                return false;

            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.Email = employeeDto.Email;
            employee.Salary = employeeDto.Salary;
            employee.DepartmentId = employeeDto.DepartmentId;

            return await _repository.UpdateAsync(id, employee);
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            return await _repository.SoftDeleteAsync(id);
        }
    }
}
