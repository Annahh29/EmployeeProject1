using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Runtime.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject1.Domain.Employees
{
    public class EmployeeManager : DomainService
    {
        private readonly IRepository<Employee, string> _employeeRepository;

        public EmployeeManager(IRepository<Employee, string> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public string GenerateID()
        {
            Random random = new Random();
            StringBuilder idBuilder = new StringBuilder();

            // Generate 2 random uppercase letters
            for (int i = 0; i < 2; i++)
            {
                char letter = (char)random.Next('A', 'Z' + 1);
                idBuilder.Append(letter);
            }

            // Generate 4 random numbers
            for (int i = 0; i < 4; i++)
            {
                int number = random.Next(0, 10);
                idBuilder.Append(number);
            }

            return idBuilder.ToString();
        }


        //public string GenerateID()
        //{
        //    string newID;
        //    do
        //    {
        //        newID = GenerateRandomID();
        //    } while (!IsIDUnique(newID));
        //    return newID;
        //}

        //private string GenerateRandomID()
        //{
        //    StringBuilder idBuilder = new StringBuilder();
        //    Random random = new Random();

        //    // Generate 2 random uppercase letters
        //    for (int i = 0; i < 2; i++)
        //    {
        //        char letter = (char)random.Next('A', 'Z' + 1);
        //        idBuilder.Append(letter);
        //    }

        //    // Generate 4 random numbers
        //    for (int i = 0; i < 4; i++)
        //    {
        //        int number = random.Next(0, 10);
        //        idBuilder.Append(number);
        //    }
        //    return idBuilder.ToString();
        //}


        //[UnitOfWork]
        //private bool IsIDUnique(string id)
        //{

        //    return _employeeRepository.GetAll().Any(e => e.Id == id);
        //}


    }
}
