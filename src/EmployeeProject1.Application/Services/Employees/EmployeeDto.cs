using EmployeeProject1.Services.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject1.Services.Employees
{
        public class EmployeeDto
        {
            public EmployeeDto()
            {
                Skills = new List<SkillDto>();
            }

        public AddressDto Address { get; set; }
        public EmployeeInputDto Employee { get; set; }
        public IList<SkillDto> Skills { get; set; }
        }
}
