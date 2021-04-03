using System;
using System.Globalization;
using CsvHelper.Configuration;
using WavePayroll.Models.FileUpload;

namespace WavePayroll.Business.FileUpload
{
    public class UploadedFileMap : ClassMap<UploadedFile>
    {
        public UploadedFileMap()
        {
            Map(m => m.Date).Name("date");
            Map(m => m.HoursWorked).Name("hours worked");
            Map(m => m.EmployeeID).Name("employee id");
            Map(m => m.JobGroup).Name("job group");
        }
    }
}
