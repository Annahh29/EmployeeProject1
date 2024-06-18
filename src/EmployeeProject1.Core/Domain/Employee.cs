using Abp.Dependency;
using Abp.Domain.Entities.Auditing;
using EmployeeProject1.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

public class Employee: AuditedEntity<string>
{
    private string _id;
    public override string Id { get; set; }
    //public override string Id
    //{
    //    get
    //    {
    //        if (string.IsNullOrEmpty(_id))
    //        {
    //            _id =   IocManager.Instance.Resolve<EmployeeManager>().GenerateID();
    //        }
    //        return _id;
    //    }
    //    set 
    //    {
    //        _id = value;
    //    }
    //}

    public Employee()
    {
        Id = IocManager.Instance.Resolve<EmployeeManager>().GenerateID();
    }

    [StringLength(100)]
    public string FirstName { get; set; }

    [StringLength(100)]
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }

    [StringLength(20)]
    public string ContactNumber { get; set; }

    [StringLength(200)]
    public string EmailAddress { get; set; }
    public Address Address { get; set; }

    
}
