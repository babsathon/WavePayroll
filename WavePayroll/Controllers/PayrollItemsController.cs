using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WavePayroll.Models;

namespace WavePayroll.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollItemsController : ControllerBase
    {
        private readonly PayrollContext _context;

        public PayrollItemsController(PayrollContext context)
        {
            _context = context;
        }

        // GET: api/PayrollItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayrollItem>>> GetPayrollItems()
        {
            return await _context.PayrollItems.ToListAsync();
        }

        // GET: api/PayrollItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PayrollItem>> GetPayrollItem(long id)
        {
            var payrollItem = await _context.PayrollItems.FindAsync(id);

            if (payrollItem == null)
            {
                return NotFound();
            }

            return payrollItem;
        }

        // PUT: api/PayrollItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayrollItem(long id, PayrollItem payrollItem)
        {
            if (id != payrollItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(payrollItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayrollItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PayrollItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PayrollItem>> PostPayrollItem(PayrollItem payrollItem)
        {
            _context.PayrollItems.Add(payrollItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPayrollItem", new { id = payrollItem.Id }, payrollItem);
        }

        // DELETE: api/PayrollItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayrollItem(long id)
        {
            var payrollItem = await _context.PayrollItems.FindAsync(id);
            if (payrollItem == null)
            {
                return NotFound();
            }

            _context.PayrollItems.Remove(payrollItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PayrollItemExists(long id)
        {
            return _context.PayrollItems.Any(e => e.Id == id);
        }
    }
}
