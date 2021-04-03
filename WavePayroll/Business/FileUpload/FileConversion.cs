using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using WavePayroll.Models.FileUpload;

namespace WavePayroll.Business.FileUpload
{
    public class FileConversion
    {
        private IFormFile FormFile;

        public FileConversion(IFormFile formFile)
        {
            FormFile = formFile;
        }

        public List<UploadedFile> ConvertFormFilesToUploadedFiles()
        {
            var uploadedFiles = new List<UploadedFile>();

            if (FormFile.Length > 0)
            {
                using (var streamReader = new StreamReader(FormFile.OpenReadStream()))
                {
                    using (CsvReader csv = new CsvReader(streamReader, CultureInfo.GetCultureInfo("en-AU")))
                    {
                        csv.Context.RegisterClassMap<UploadedFileMap>();
                        uploadedFiles = csv.GetRecords<UploadedFile>().ToList();            
                    }
                    uploadedFiles.ForEach(c => c.ReportID = GetReportId(FormFile.FileName));
                  
                }
            }
           
            return uploadedFiles;
        }

        public int GetReportId(string filename)
        {

            return Int32.Parse(Regex.Match(filename, @"\d+").Value);
        }
    }
}
