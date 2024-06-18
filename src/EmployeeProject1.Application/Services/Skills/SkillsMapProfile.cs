using AutoMapper;
using EmployeeProject1.Domain;
using EmployeeProject1.Services.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject1.Services.Skills
{
    public class SkillsMapProfile: Profile
    {
        public SkillsMapProfile()
        {
            CreateMap<SkillDto, Skill>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); ;
        }
    }
}
