using Microsoft.EntityFrameworkCore;
using WavePayroll.Models.FileUpload;

namespace WavePayroll.Data
{
    public class PayrollContext : DbContext
    {
        public PayrollContext(DbContextOptions<PayrollContext> options) : base(options)
        {
        }

        public DbSet<UploadedFile> UploadedFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UploadedFile>().ToTable("UploadedFile");
        }
    }
}
