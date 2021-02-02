using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Models
{
    public class EmployeeSkill
    {
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public int SkillId { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
