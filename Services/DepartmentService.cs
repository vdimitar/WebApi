using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.IServices;
using WebApi.Models;

namespace WebApi.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IBaseRepository<Department> _repository;

        public DepartmentService
            (
            IBaseRepository<Department> repository
            )
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Department> CreateDepartment(Department department)
        {
            return await _repository.AddAsync(department);
        }

        public async Task<bool> UpdateDepartment(int id, Department department)
        {
            return await _repository.UpdateAsync(id, department);
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            return await _repository.SoftDeleteAsync(id);
        }
    }
}
