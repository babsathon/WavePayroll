using System.Collections.Generic;

namespace WavePayroll.Business.Report.Payroll
{
    public class PayrollReport
    {
        public List<EmpolyeeReport> EmployeeReports { get; set; }

        public PayrollReport(List<EmpolyeeReport> employeeReports)
        {
            EmployeeReports = employeeReports;
        }

        
    }
}
