using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject1.Services.Employees
{
    [AutoMap(typeof(Employee))]
    public class EmployeeInputDto: EntityDto<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid AddressId { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
