using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.IServices;
using WebApi.Models;

namespace WebApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IBaseRepository<Employee> _repository;

        public EmployeeService
            (
            IBaseRepository<Employee> repository
            )
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            return await _repository.AddAsync(employee);
        }

        public async Task<bool> UpdateEmployee(int id, Employee employee)
        {
            return await _repository.UpdateAsync(id, employee);
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            return await _repository.SoftDeleteAsync(id);
        }
    }
}
