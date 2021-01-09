using Dapper;
using EmployeesManagementSystem.Data;
using EmployeesManagementSystem.IService;
using EmployeesManagementSystem.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Service
{
    public class DayEventService : IDayEventService
    {
        DayEvent dayEvent = new DayEvent();
        public IConfiguration Configuration { get; }
        List<DayEvent> dayEvents = new List<DayEvent>();
        public DayEventService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string Delete(int id)
        {
            string message = "";
            try
            {
                dayEvent = new DayEvent()
                {
                    DayEventId = id
                };
                using (IDbConnection con = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                {
                    if (con.State == ConnectionState.Closed) con.Open();
                    var dayEvents = con.Query<DayEvent>("SP_DayEvent",
                       this.SetParameters(dayEvent, (int)OperationType.Delete),
                       commandType: CommandType.StoredProcedure);
                    message = "Deleted";
                }
            }
            catch (Exception ex)
            {

                message = ex.Message;
            }
            return message;
        }

        public DayEvent GetEvent(DateTime eventDate)
        {
            dayEvent = new DayEvent();
            using (IDbConnection con = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                string sql = string.Format(@"SELECT * FROM DayEvent WHERE EventDate = '{0}'",eventDate.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture));
                var dayEvents = con.Query<DayEvent>(sql).ToList();

                if (dayEvents != null && dayEvents.Count() > 0)
                {
                    dayEvent = dayEvents.SingleOrDefault();
                }
                else
                {
                    dayEvent.EventDate = eventDate;
                    dayEvent.FromDate = eventDate;
                    dayEvent.ToDate = eventDate;
                }
            }
            return dayEvent;
        }

        public List<DayEvent> GetEvents(DateTime fromDate, DateTime toDate)
        {
            dayEvents = new List<DayEvent>();
            using (IDbConnection con = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                string sql = string.Format(@"SELECT * FROM DayEvent WHERE EventDate BETWEEN '{0}' AND '{1}'", fromDate.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture), toDate.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture));
                var dayEvents = con.Query<DayEvent>(sql).ToList();

                if (dayEvents != null && dayEvents.Count() > 0)
                {
                    dayEvent = dayEvents.FirstOrDefault();
                }
            }
            return dayEvents;
        }
        public DayEvent SaveOrUpdate(DayEvent dayEvent)
        {
            this.dayEvent = new DayEvent();
            try
            {
                int operationType = Convert.ToInt32(dayEvent.DayEventId == 0 ? OperationType.Insert : OperationType.Update);
                using (IDbConnection con = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                {
                    if (con.State == ConnectionState.Closed) con.Open();
                    var dayEvents = con.Query<DayEvent>("SP_DayEvent",
                        this.SetParameters(dayEvent, operationType),
                        commandType: CommandType.StoredProcedure);
                    if (dayEvents != null && dayEvents.Count()>0)
                    {
                        dayEvent = dayEvents.FirstOrDefault();
                    }

                }
            }
            catch (Exception e)
            {

                dayEvent.Message = e.Message;
            }
            return dayEvent;
        }
        private DynamicParameters SetParameters(DayEvent dayEvent, int operationType)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@DayEventId", dayEvent.DayEventId);
            parameters.Add("@Note", dayEvent.Note);
            parameters.Add("@EventDate", dayEvent.EventDate);
            parameters.Add("@OperationType", operationType);
            return parameters;

        }
    }
}
