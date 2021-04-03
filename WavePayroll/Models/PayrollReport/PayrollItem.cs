using System;
namespace WavePayroll.Models
{
    public class PayrollItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
