using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public abstract class BaseEntity
    {
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
