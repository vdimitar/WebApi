using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.DTOs;

namespace WebApi.IServices
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetAllEmployees();
        Task<EmployeeDTO> GetEmployeeById(int id);
        Task<EmployeeDTO> CreateEmployee(EmployeeDTO employeeDto);
        Task<bool> UpdateEmployee(int id, EmployeeDTO employeeDto);
        Task<bool> DeleteEmployee(int id);
    }
}
