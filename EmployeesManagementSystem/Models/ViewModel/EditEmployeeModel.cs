using EmployeesManagementSystem.Models.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Models
{
    public class EditEmployeeModel
    {

        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Please provide First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please provide Last Name")]
        [MinLength(2)]
        public string LastName { get; set; }
        [EmailAddress]
        [EmailDomainValidator(AllowedDomain = "gmail.com", ErrorMessage = "The domain must be: gmail.com")]

        public string Email { get; set; }
    
        public string ConfirmEmail { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public int DepartmentId { get; set; }
        public string PhotoPath { get; set; }
        //[ValidateComplexType]
        public Department Department { get; set; } = new Department();

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
