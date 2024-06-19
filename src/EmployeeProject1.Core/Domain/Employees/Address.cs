using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject1.Domain.Employees
{
    public class Address : AuditedEntity<Guid>
    {
        [StringLength(200)]
        public string Street { get; set; }

        [StringLength(200)]
        public string City { get; set; }

        [StringLength(200)]
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
