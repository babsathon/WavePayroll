using Microsoft.EntityFrameworkCore;

namespace WavePayroll.Models
{
    public class PayrollContext : DbContext
    {
        public PayrollContext(DbContextOptions<PayrollContext> options)
            : base(options)
        {
        }

        public DbSet<PayrollItem> PayrollItems { get; set; }
    }
}
