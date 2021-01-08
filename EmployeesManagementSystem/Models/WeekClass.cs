using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Models
{
    public class WeekClass
    {
        public List<DayEvent> Dates { get; set; } = new List<DayEvent>();
    }
}
