using Abp.Domain.Entities.Auditing;
using EmployeeProject1.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject1.Domain
{
    public class Skill: AuditedEntity<Guid>
    {
        [StringLength(1000)]
        public string Name { get; set; }
        public Employee Employee { get; set; }
        public RefListYearsOfExperience YearsOfExperience  { get; set; }
        public RefListSeniorityRating SeniorityRating { get; set; }
    }
}
