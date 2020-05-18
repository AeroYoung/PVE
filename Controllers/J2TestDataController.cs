using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PVE.Data;
using PVE.Models;

namespace PVE.Controllers
{
    public class J2TestDataController : Controller
    {
        private readonly PVEContext _context;

        public J2TestDataController(PVEContext context)
        {
            _context = context;
        }

        // GET: J2TestData
        public async Task<IActionResult> Index()
        {
            var pVEContext = _context.J2TestData.Include(j => j.PveData);
            return View(await pVEContext.ToListAsync());
        }

        // GET: J2TestData/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var j2TestData = await _context.J2TestData
                .Include(j => j.PveData)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (j2TestData == null)
            {
                return NotFound();
            }

            return View(j2TestData);
        }

        // GET: J2TestData/Create
        public IActionResult Create()
        {
            ViewData["PveDataID"] = new SelectList(_context.PveData, "ID", "SerialNum");
            return View();
        }

        // POST: J2TestData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PveDataID,GroupName,TestGroup,ActualTestDate,SoftVersion,SABVersion,Remark")] J2TestData j2TestData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(j2TestData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PveDataID"] = new SelectList(_context.PveData, "ID", "SerialNum", j2TestData.PveDataID);
            return View(j2TestData);
        }

        // GET: J2TestData/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var j2TestData = await _context.J2TestData.FindAsync(id);
            if (j2TestData == null)
            {
                return NotFound();
            }
            ViewData["PveDataID"] = new SelectList(_context.PveData, "ID", "SerialNum", j2TestData.PveDataID);
            return View(j2TestData);
        }

        // POST: J2TestData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PveDataID,GroupName,TestGroup,ActualTestDate,SoftVersion,SABVersion,Remark")] J2TestData j2TestData)
        {
            if (id != j2TestData.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(j2TestData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!J2TestDataExists(j2TestData.ID))
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
            ViewData["PveDataID"] = new SelectList(_context.PveData, "ID", "SerialNum", j2TestData.PveDataID);
            return View(j2TestData);
        }

        // GET: J2TestData/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var j2TestData = await _context.J2TestData
                .Include(j => j.PveData)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (j2TestData == null)
            {
                return NotFound();
            }

            return View(j2TestData);
        }

        // POST: J2TestData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var j2TestData = await _context.J2TestData.FindAsync(id);
            _context.J2TestData.Remove(j2TestData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool J2TestDataExists(int id)
        {
            return _context.J2TestData.Any(e => e.ID == id);
        }
    }
}
