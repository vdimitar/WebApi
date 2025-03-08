using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public DepartmentDTO Department { get; set; }
        public List<EmployeeProjectDTO> EmployeeProjects { get; set; } = new List<EmployeeProjectDTO>();

    }
}
