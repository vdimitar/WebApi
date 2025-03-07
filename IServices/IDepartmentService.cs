using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.IServices
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllDepartments();
        Task<Department> GetDepartmentById(int id);
        Task<Department> CreateDepartment(Department department);
        Task<bool> UpdateDepartment(int id, Department department);
        Task<bool> DeleteDepartment(int id);
    }
}
