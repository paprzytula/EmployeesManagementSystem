using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Models
{
    public class EmployeeSkill
    {
        public string EmployeesId { get; set; }
        public virtual Employee Employee { get; set; }
        public int SkillsId { get; set; }
        public virtual Skill Skill { get; set; }
        public string Answer { get; set; }
    }
}
