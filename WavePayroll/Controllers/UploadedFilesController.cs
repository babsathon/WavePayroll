using Microsoft.AspNetCore.Mvc;
using WavePayroll.Data;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using System;
using WavePayroll.Business.FileUpload;

namespace WavePayroll.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class UploadedFilesController : ControllerBase
    {
        private readonly PayrollContext _context;

        public UploadedFilesController(PayrollContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> OnPostUploadAsync([FromForm] IFormFile file)
        {
            
            var fileConversion = new FileConversion(file);
            var reportId = fileConversion.GetReportId(file.FileName);

            if (_context.UploadedFiles.Any(o => o.ReportID == reportId))
            {
                throw new Exception("Error: Report Id " + reportId + " already exists in database. Cannot upload duplicate file"); 
            }
            var uploadedFiles = fileConversion.ConvertFormFilesToUploadedFiles();

            foreach (var uploadedFile in uploadedFiles)
            {
                _context.UploadedFiles.Add(uploadedFile);
                await _context.SaveChangesAsync();
            }

            return Ok();
            
        }
    }
}
