using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs
{
    public class EmployeeProjectDTO
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Role { get; set; }
        public ProjectDTO Project { get; set; }
    }
}
