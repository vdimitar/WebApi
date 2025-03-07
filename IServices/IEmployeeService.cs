using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.IServices
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> CreateEmployee(Employee employee);
        Task<bool> UpdateEmployee(int id, Employee employee);
        Task<bool> DeleteEmployee(int id);
    }
}
