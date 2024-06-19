using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EmployeeProject1.Domain.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject1.Services.Skills
{
    [AutoMap(typeof(Skill))]
    public class SkillDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public int YearsOfExperience { get; set; }
        public int SeniorityRating { get; set; }
    }
}
