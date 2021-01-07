using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Models
{
    public class Schedule
    {

        public int Id { get; set; }
        public bool IsAttended { get; set; }
        public DateTime Date { get; set; }
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        
    }
}
