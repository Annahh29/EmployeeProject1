using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Validation;
using Abp.UI;
using EmployeeProject1.Domain;
using EmployeeProject1.Domain.Enums;
using EmployeeProject1.Services.Skills;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeProject1.Services.Employees
{
    public class EmployeeAppService : ApplicationService
    {
        private readonly IRepository<Employee, string> _employeeRepository;
        private readonly IRepository<Address, Guid> _addressRepository;
        private readonly IRepository<Skill, Guid> _skillRepository;
        private readonly EmployeeManager _employeeManager;
        private readonly IUnitOfWorkManager _unitOfWork;

        public EmployeeAppService(IRepository<Employee, string> employeeRepository, IRepository<Address, Guid> addressRepository,
            IRepository<Skill, Guid> skillRepository, EmployeeManager employeeManager, IUnitOfWorkManager unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _addressRepository = addressRepository;
            _skillRepository = skillRepository;
            _employeeManager = employeeManager;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]

        public async Task<EmployeeDto> CreateEmployeeIncludingSkillsAsync(EmployeeDto input)
        {
            try
            {
                if (input == null)
                {
                    throw new UserFriendlyException("Please provide valid input data.");
                }

                // Check if address is not null and map AddressDto to Address entity and insert it
                if (input.Address == null)
                {
                    throw new UserFriendlyException("Please provide address details.");
                }

                // Start a transaction
                using (var uow = _unitOfWork.Begin())
                {

                    var address = ObjectMapper.Map<Address>(input.Address);
                    address = await _addressRepository.InsertAsync(address);

                    // Map EmployeeDto to Employee entity and insert it

                    var employee = new Employee();
                    employee = ObjectMapper.Map(input.Employee, employee);
                    await ValidateEmployee(employee);
                    employee.Address = address; // Ensure the relationship is properly set
                    employee = await _employeeRepository.InsertAsync(employee);

                    //await CurrentUnitOfWork.SaveChangesAsync();

                    if (input.Skills.IsNullOrEmpty())
                    {
                        throw new UserFriendlyException("Please provide skills details.");
                    }
                    var skillDtos = new List<SkillDto>();

                    // Insert skills and establish relationship with employee
                    foreach (var skillDto in input.Skills)
                    {

                        var skill = ObjectMapper.Map<Skill>(skillDto);
                        skill.Employee = employee; // Ensure the relationship is properly set
                        skill = await _skillRepository.InsertAsync(skill);
                        skillDtos.Add(ObjectMapper.Map<SkillDto>(skill));
                    }

                    await uow.CompleteAsync();

                    var resultEmployeeDto = new EmployeeDto();
                    resultEmployeeDto.Employee = ObjectMapper.Map<EmployeeInputDto>(employee);
                    resultEmployeeDto.Address = ObjectMapper.Map<AddressDto>(address);
                    resultEmployeeDto.Skills = skillDtos;

                    return resultEmployeeDto;

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }    

        }

        [HttpPut]
        public async Task<EmployeeDto> UpdateEmployeeAsync(EmployeeDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("Please provide valid input data.");
            }

            // Fetch the existing employee entity
            var existingEmployee = await _employeeRepository.GetAsync(input.Employee.Id);
            if (existingEmployee == null)
            {
                throw new UserFriendlyException("Employee not found.");
            }

            // Update the employee's properties
            var employee = ObjectMapper.Map<Employee>(input.Employee);


            // Update the address if provided
            if (input.Address != null)
            {
                var existingAddress = existingEmployee.Address;
                if (existingAddress == null)
                {
                    var newAddress = ObjectMapper.Map<Address>(input.Address);
                    existingEmployee.Address = await _addressRepository.InsertAsync(newAddress);
                }
                else
                {
                    existingAddress = ObjectMapper.Map<Address>(input.Address);
                    await _addressRepository.UpdateAsync(existingAddress);
                }
            }

            // Update skills
            if (input.Skills != null)
            {
                // Remove skills that are not in the input list
                var existingSkills = await _skillRepository.GetAllListAsync(s => s.Employee.Id == existingEmployee.Id);
                var inputSkillIds = input.Skills.Select(s => s.Id).ToList();
                foreach (var existingSkill in existingSkills)
                {
                    if (!inputSkillIds.Contains(existingSkill.Id))
                    {
                        await _skillRepository.DeleteAsync(existingSkill);
                    }
                }

                // Add or update skills
                foreach (var skillDto in input.Skills)
                {
                    if (skillDto.Id == null)
                    {
                        // New skill
                        var newSkill = ObjectMapper.Map<Skill>(skillDto);
                        newSkill.Employee = existingEmployee;
                        await _skillRepository.InsertAsync(newSkill);
                    }
                    else
                    {
                        // Existing skill
                        var existingSkill = existingSkills.FirstOrDefault(s => s.Id == skillDto.Id);
                        if (existingSkill != null)
                        {
                            existingSkill.Name = skillDto.Name;
                            existingSkill.YearsOfExperience = (RefListYearsOfExperience)skillDto.YearsOfExperience;
                            existingSkill.SeniorityRating = (RefListSeniorityRating)skillDto.SeniorityRating;
                            await _skillRepository.UpdateAsync(existingSkill);
                        }
                    }
                }
            }

            // Update the employee in the repository
            await _employeeRepository.UpdateAsync(existingEmployee);

            // Map back the updated employee to EmployeeDto
            var resultEmployeeDto = ObjectMapper.Map<EmployeeDto>(existingEmployee);
            if (existingEmployee.Address != null)
            {
                resultEmployeeDto.Address = ObjectMapper.Map<AddressDto>(existingEmployee.Address);
            }

            var updatedSkills = await _skillRepository.GetAllListAsync(s => s.Employee.Id == existingEmployee.Id);
            resultEmployeeDto.Skills = ObjectMapper.Map<List<SkillDto>>(updatedSkills);

            return resultEmployeeDto;
        }

        [HttpDelete]
        public async Task DeleteEmployeeAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new UserFriendlyException("Please provide a valid employee ID.");
            }

            // Fetch the existing employee entity
            var existingEmployee = await _employeeRepository.GetAllIncluding(e => e.Address).FirstOrDefaultAsync(e => e.Id == id);
            if (existingEmployee == null)
            {
                throw new UserFriendlyException("Employee not found.");
            }

            // Delete associated skills
            var existingSkills = await _skillRepository.GetAllListAsync(s => s.Employee.Id == id);
            foreach (var skill in existingSkills)
            {
                await _skillRepository.DeleteAsync(skill);
            }

            // Delete associated address if needed
            if (existingEmployee.Address != null)
            {
                await _addressRepository.DeleteAsync(existingEmployee.Address);
            }

            // Delete the employee
            await _employeeRepository.DeleteAsync(existingEmployee);
        }

        [HttpGet]
        public async Task<EmployeeDto> GetEmployeeAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new UserFriendlyException("Please provide a valid employee ID.");
            }

            // Fetch the existing employee entity
            var employee = await _employeeRepository.GetAllIncluding(e => e.Address).FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                throw new UserFriendlyException("Employee not found.");
            }

            // Fetch associated skills
            var skills = await _skillRepository.GetAllListAsync(s => s.Employee.Id == id);

            // Map the employee and associated data to EmployeeDto
            var employeeDto = new EmployeeDto();
            employeeDto.Employee = ObjectMapper.Map<EmployeeInputDto>(employee);
            if (employee.Address != null)
            {
                employeeDto.Address = ObjectMapper.Map<AddressDto>(employee.Address);
            }
            employeeDto.Skills = ObjectMapper.Map<List<SkillDto>>(skills);

            return employeeDto;
        }

        [HttpGet]
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            // Fetch all employees including their addresses and skills
            var employees = await _employeeRepository.GetAllIncluding(e => e.Address).ToListAsync();
            return employees;
        }

        [HttpGet]
        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            // Fetch all employees including their addresses
            var employees = await _employeeRepository.GetAllIncluding(e => e.Address).ToListAsync();

            if (employees == null || !employees.Any())
            {
                throw new UserFriendlyException("No employees found.");
            }

            var employeeDtos = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
                // Fetch associated skills for each employee
                var skills = await _skillRepository.GetAllListAsync(s => s.Employee.Id == employee.Id);

                // Map the employee and associated data to EmployeeDto
                var employeeDto = new EmployeeDto
                {
                    Employee = ObjectMapper.Map<EmployeeInputDto>(employee),
                    Address = employee.Address != null ? ObjectMapper.Map<AddressDto>(employee.Address) : null,
                    Skills = ObjectMapper.Map<List<SkillDto>>(skills)
                };

                employeeDtos.Add(employeeDto);
            }

            return employeeDtos;
        }


        public async Task<List<Employee>> GetBySearch(string term)
        {

            var searchResult = _employeeRepository.GetAllIncluding(e => e.Address).Where(e => e.FirstName.ToLower().Contains(term.ToLower())
            || e.LastName.ToLower().Contains(term.ToLower())
            || e.EmailAddress.ToLower().Contains(term.ToLower()));
            var result = searchResult.Take(10);
            return ObjectMapper.Map<List<Employee>>(searchResult);
        }

        public async Task ValidateEmployee(Employee input)
        {
            if (input == null)
            {
                return;
            }

            var validationResults = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(input.FirstName))
                validationResults.Add(new ValidationResult("First Name is mandatory"));
            if (string.IsNullOrWhiteSpace(input.LastName))
                validationResults.Add(new ValidationResult("Last Name is mandatory"));

            // email and mobile number must be unique
            if (await MobileNoAlreadyInUse(input.ContactNumber, null))
                validationResults.Add(new ValidationResult("Specified mobile number already used by another person"));
            if (await EmailAlreadyInUse(input.EmailAddress, null))
                validationResults.Add(new ValidationResult("Specified email already used by another person"));

            if (validationResults.Any())
                throw new AbpValidationException("Please correct the errors and try again", validationResults);
        }


        /// <summary>
        /// Checks is specified mobile number already used by another person
        /// </summary>
        /// <returns></returns>
        private async Task<bool> MobileNoAlreadyInUse(string mobileNo, string id)
        {
            if (string.IsNullOrWhiteSpace(mobileNo))
                return false;

            var employees = await _employeeRepository.GetAll().ToListAsync();

            return employees.Any(e =>
                               e.ContactNumber.Trim().ToLower() == mobileNo.Trim().ToLower() && (id == null || e.Id != id)) || false;

        }

        /// <summary>
        /// Checks is specified email already used by another person
        /// </summary>
        /// <returns></returns>
        private async Task<bool> EmailAlreadyInUse(string email, string id)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var employees = await _employeeRepository.GetAll().ToListAsync();

            return employees.Any(e =>
           e.EmailAddress.Trim().ToLower() == email.Trim().ToLower() && (id == null || e.Id != id));

        }

    }
}
