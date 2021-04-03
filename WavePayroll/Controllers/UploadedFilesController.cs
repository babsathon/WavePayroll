using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WavePayroll.Data;
using WavePayroll.Models.FileUpload;

namespace WavePayroll
{
    public class UploadedFilesController : Controller
    {
        private readonly PayrollContext _context;

        public UploadedFilesController(PayrollContext context)
        {
            _context = context;
        }

        // GET: UploadedFiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.UploadedFiles.ToListAsync());
        }

        // GET: UploadedFiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uploadedFile = await _context.UploadedFiles
                .FirstOrDefaultAsync(m => m.UploadedFileID == id);
            if (uploadedFile == null)
            {
                return NotFound();
            }

            return View(uploadedFile);
        }

        // GET: UploadedFiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UploadedFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UploadedFileID,Date,HoursWorked,EmployeeID,JobGroup")] UploadedFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uploadedFile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uploadedFile);
        }

        // GET: UploadedFiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uploadedFile = await _context.UploadedFiles.FindAsync(id);
            if (uploadedFile == null)
            {
                return NotFound();
            }
            return View(uploadedFile);
        }

        // POST: UploadedFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UploadedFileID,Date,HoursWorked,EmployeeID,JobGroup")] UploadedFile uploadedFile)
        {
            if (id != uploadedFile.UploadedFileID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uploadedFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UploadedFileExists(uploadedFile.UploadedFileID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(uploadedFile);
        }

        // GET: UploadedFiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uploadedFile = await _context.UploadedFiles
                .FirstOrDefaultAsync(m => m.UploadedFileID == id);
            if (uploadedFile == null)
            {
                return NotFound();
            }

            return View(uploadedFile);
        }

        // POST: UploadedFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uploadedFile = await _context.UploadedFiles.FindAsync(id);
            _context.UploadedFiles.Remove(uploadedFile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UploadedFileExists(int id)
        {
            return _context.UploadedFiles.Any(e => e.UploadedFileID == id);
        }
    }
}
