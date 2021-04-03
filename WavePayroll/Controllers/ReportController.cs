using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WavePayroll.Business.Report;
using WavePayroll.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WavePayroll.Controllers
{
    [Route("api/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly PayrollContext _context;

        public ReportController(PayrollContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Report()
        {
            var report = new Report(_context);


            return Ok(new { PayrollReport = report.GetPayrollReport() });
        }
       
    }
}
