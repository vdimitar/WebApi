using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Project : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Budget { get; set; }

        public string ProjectCode { get; set; }  

        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
