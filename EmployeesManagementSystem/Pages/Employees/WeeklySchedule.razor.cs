using EmployeesManagementSystem.Data;
using EmployeesManagementSystem.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Pages.Employees
{
    public class WeeklyScheduleBase : ComponentBase
    {

        [Inject]
        public ApplicationDbContext Db { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected List<Employee> Employees = new List<Employee>();

        protected List<ScheduleDetail> Schedule = new List<ScheduleDetail>();

        protected List<DateTime?> Dates = new List<DateTime?>();
        protected override void OnInitialized()
        {
            Employees = Db.Users.ToList();
            InitializeSchedule();
        }

        private void InitializeSchedule()
        {
            var currentDayNumber = (int)DateTime.Now.DayOfWeek;
            if (currentDayNumber == 0)
                currentDayNumber = 7;

            var firstWeekDay = DateTime.Now.AddDays(-currentDayNumber);

            foreach (var item in Employees)
            {
                var scheduleDetail = new ScheduleDetail();
                scheduleDetail.Employee = item;
                scheduleDetail.Days = new List<ScheduleDay>();

                for (int i = 0; i < 7; i++)
                {
                    var date = firstWeekDay.AddDays(i).Date;
                    if (!Dates.Any(d => date == d))
                        Dates.Add(date);

                    var existingSchedule = Db.Schedules.SingleOrDefault(s => s.EmployeeId == item.Id && s.Date == date);
                    scheduleDetail.Days.Add(new ScheduleDay
                    {
                        Date = firstWeekDay.AddDays(i),
                        IsAttended = existingSchedule != null ? existingSchedule.IsAttended : false,
                        IsExist = existingSchedule != null,
                        DayOfWeek = i + 1,
                    });
                }
                Schedule.Add(scheduleDetail);
            }
        }
    }

    public class ScheduleDetail
    {
        public Employee Employee { get; set; }

        public List<ScheduleDay> Days { get; set; }
    }

    public class ScheduleDay
    {
        public DateTime Date { get; set; }
        public bool IsAttended { get; set; }
        public bool IsExist { get; set; }
        public int DayOfWeek { get; set; }
    }
}
