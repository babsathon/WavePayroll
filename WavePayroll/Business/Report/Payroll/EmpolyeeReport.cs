using System;
namespace WavePayroll.Business.Report.Payroll
{
    public class EmpolyeeReport
    {
        public string EmployeeId { get; set; }
        public PayPeriod PayPeriod { get; set; }
        public  string AmountPaid { get; set; }

        public EmpolyeeReport(string employeeID, PayPeriod payPeriod, string amountPaid)
        {
            EmployeeId = employeeID;
            PayPeriod = payPeriod;
            AmountPaid = amountPaid;
        }
        
    }
}