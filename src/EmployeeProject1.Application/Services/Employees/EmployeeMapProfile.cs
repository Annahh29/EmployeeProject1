using AutoMapper;

namespace EmployeeProject1.Services.Employees
{
    public class EmployeeMapProfile : Profile
    {
        public EmployeeMapProfile()
        {
            CreateMap<EmployeeInputDto, Employee>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); ;
        }
    }
}
