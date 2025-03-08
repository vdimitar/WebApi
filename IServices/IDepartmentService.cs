using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.DTOs;
using WebApi.Models;

namespace WebApi.IServices
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDTO>> GetAllDepartments();
        Task<DepartmentDTO> GetDepartmentById(int id);
        Task<DepartmentDTO> CreateDepartment(DepartmentDTO departmentDto);
        Task<bool> UpdateDepartment(int id, DepartmentDTO departmentDto);
        Task<bool> DeleteDepartment(int id);
        Task<decimal> GetTotalBudgetByDepartmentId(int departmentId);
    }
}
