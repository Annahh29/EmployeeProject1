﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EmployeeProject1.Domain.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject1.Services.Employees
{
    [AutoMap(typeof(Address))]
    public class AddressDto : EntityDto<Guid>
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
