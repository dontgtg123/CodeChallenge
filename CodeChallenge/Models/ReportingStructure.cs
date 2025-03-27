using System.Collections.Generic;

namespace CodeChallenge.Models
{
    //A ReportingStructure holds information for the number of reporting employees a specific employee has
    public class ReportingStructure
    {
        public Employee employee { get; set; }
        public int numberOfReports { get; set; }

        public ReportingStructure(Employee employee, int numberOfReports)
        {
            this.employee = employee;

            this.numberOfReports = numberOfReports;
        }
    }
}