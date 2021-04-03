using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using WavePayroll.Business.Report.Payroll;
using WavePayroll.Data;

namespace WavePayroll.Business.Report
{
    public class Report
    {
        PayrollContext PayrollContext;

        public Report(PayrollContext payrollContext)
        {
            PayrollContext = payrollContext;
        }

        public PayrollReport GetPayrollReport()
        {
            var uploadedFiles = PayrollContext.UploadedFiles.ToList();

            var orderedRows = uploadedFiles.OrderBy(p => p.EmployeeID).ThenBy(x => x.Date).ToList();

            var idGroups = orderedRows.GroupBy(x => x.EmployeeID).ToList();

            var employeeReports = new List<EmpolyeeReport>();
            var payrollReport = new PayrollReport(employeeReports);


            foreach (var idGroup in idGroups)
            {

                foreach (var id in idGroup)
                {
                    var payPeriodStartDate = new DateTime(id.Date.Year, id.Date.Month, 1).ToString("yyyy-MM-dd");
                    var payPeriodEndDate = new DateTime(id.Date.Year, id.Date.Month, 15).ToString("yyyy-MM-dd");

                    if (id.Date.Day > 15)
                    {
                        payPeriodStartDate = new DateTime(id.Date.Year, id.Date.Month, 16).ToString("yyyy-MM-dd");
                        var lastDayOfMonth = DateTime.DaysInMonth(id.Date.Year, id.Date.Month);
                        payPeriodEndDate = new DateTime(id.Date.Year, id.Date.Month, lastDayOfMonth).ToString("yyyy-MM-dd");
                    }

                    var employeeReport = employeeReports.FirstOrDefault(
                        x => x.EmployeeId.Equals(id.EmployeeID.ToString())
                        && x.PayPeriod.StartDate.Equals(payPeriodStartDate)
                        && x.PayPeriod.EndDate.Equals(payPeriodEndDate));

                    if (employeeReport != null)
                    {
                        double totalPaid = GetAmountPaidFromFormattedTotal(employeeReport.AmountPaid) +
                            GetAmountPaidFromFormattedTotal(CalculateAmountPaidPerDay(id.JobGroup, id.HoursWorked));
                        employeeReport.AmountPaid = totalPaid.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
                    }
                    else
                    {
                        employeeReports.Add(new EmpolyeeReport(id.EmployeeID.ToString(),
                            new PayPeriod(payPeriodStartDate, payPeriodEndDate),
                            CalculateAmountPaidPerDay(id.JobGroup, id.HoursWorked)));
                    }
                }
            }

            return payrollReport;
        }

        private string CalculateAmountPaidPerDay(char jobGroup, double hoursWorked)
        {
            double amountPaid = 0;

            if (jobGroup == 'A')
            {
                amountPaid = 20 * hoursWorked;
            }
            else
            {
                amountPaid = 30 * hoursWorked;
            }

            return amountPaid.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
        }

        private double GetAmountPaidFromFormattedTotal(string totalPaid)
        {
            return Double.Parse(Regex.Match(totalPaid, @"\d+").Value);
        }

    }
}
