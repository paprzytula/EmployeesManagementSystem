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
        protected bool ShowAlert { get; set; } = false;
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

            var firstWeekDay = DateTime.Now.AddDays(-currentDayNumber + 1);

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

        protected void SaveSchedule()
        {
            foreach (var item in Schedule)
            {
                foreach (var day in item.Days)
                {
                    // Get the day from the database 
                    if (day.IsExist)
                    {
                        var existingDay = Db.Schedules.SingleOrDefault(d => d.Date == day.Date.Date && d.EmployeeId == item.Employee.Id);
                        existingDay.IsAttended = day.IsAttended;
                    }
                    else
                    {
                        if (day.IsAttended)
                        {
                            Db.Schedules.Add(new Models.Schedule
                            {
                                IsAttended = true,
                                EmployeeId = item.Employee.Id,
                                Date = day.Date.Date,
                            });
                        }
                    }

                }
            }
            Db.SaveChanges();
            ShowAlert = true; 

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
