using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Employee : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public decimal Salary { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
