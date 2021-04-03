using System;
namespace WavePayroll.Business.Report.Payroll
{
    public class PayPeriod
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public PayPeriod(string startDate, string endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
