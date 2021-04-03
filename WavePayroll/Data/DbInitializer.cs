using System;
using System.Linq;

using WavePayroll.Models.FileUpload;

namespace WavePayroll.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PayrollContext context)
        {
            context.Database.EnsureCreated();

            // Look for any uploaded files.
            if (context.UploadedFiles.Any())
            {
                return;   // DB has been seeded
            }

            //Placeholder files, will be replaced on first uploads by api.
            var uploadedFiles = new UploadedFile();

            context.SaveChanges();
        }
    }
}
