using System;
using System.ComponentModel.DataAnnotations;

namespace WavePayroll.Models.FileUpload
{
    public class UploadedFile
    {
        [Key]
        public int UploadedFileID { get; set; }
        public int ReportID { get; set; }
        public DateTime Date { get; set; }
        public double HoursWorked { get; set; }
        public int EmployeeID { get; set; }
        public char JobGroup { get; set; }
    }
}
