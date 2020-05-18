using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PVE.Data;
using PVE.Models;

namespace PVE.Controllers
{
    public class J2TestDataController : BaseController
    {
        public J2TestDataController(PVEContext context) : base(context)
        {
        }

        #region Index & Details

        [AllowAnonymous]
        public async Task<IActionResult> Index(int? foreignKey)
        {
            if (!GetPveData(foreignKey, out _))
                return RedirectToAction("Index", "PveDatas");

            var datas = from m in _context.J2TestData
                select m;

            datas = datas.Where(s => s.PveData.ID.Equals(foreignKey.Value)).OrderBy(s => s.ID)
                .Include(d => d.PveData);

            return View(await datas.ToListAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id, int? foreignKey)
        {
            if (id == null)
                return NotFound();

            if (!GetPveData(foreignKey, out _))
                return RedirectToAction("Index", "PveDatas");

            var signal = await _context.J2TestData
                .FirstOrDefaultAsync(m => m.ID == id);
            if (signal == null)
            {
                return NotFound();
            }
            return View(signal);
        }

        #endregion

        [Authorize(Roles = Constants.AdministratorRole)]
        public IActionResult Create(int? foreignKey)
        {
            if (!GetPveData(foreignKey, out _))
                return RedirectToAction("Index", "PveDatas");

            return View();
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ID,PveDataID,GroupName,TestGroup,ActualTestDate,SoftVersion,SABVersion,Remark")] J2TestData j2TestData,
            int? foreignKey)
        {
            if (!ModelState.IsValid)
                return View(j2TestData);

            if (!GetPveData(foreignKey, out var pveData))
                return RedirectToAction("Index", "PveDatas");

            j2TestData.PveData = pveData;
            _context.Add(j2TestData);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { foreignKey = pveData.ID });
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        public async Task<IActionResult> Edit(int? id, int? foreignKey)
        {
            if (id == null)
                return NotFound();

            if (!GetPveData(foreignKey, out _))
                return RedirectToAction("Index", "PveDatas");

            var signal = await _context.J2TestData.FindAsync(id);
            if (signal == null)
            {
                return NotFound();
            }
            return View(signal);
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, 
            [Bind("ID,PveDataID,GroupName,TestGroup,ActualTestDate,SoftVersion,SABVersion,Remark")] J2TestData j2TestData,
            int? foreignKey)
        {
            if (id != j2TestData.ID)
            {
                return NotFound();
            }

            if (!GetPveData(foreignKey, out _))
                return RedirectToAction("Index", "PveDatas");

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
                return RedirectToAction(nameof(Index), new { foreignKey });
            }
            return View(j2TestData);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var j2TestData = await _context.J2TestData
                .FirstOrDefaultAsync(m => m.ID == id);
            if (j2TestData == null)
            {
                return NotFound();
            }

            return View(j2TestData);
        }

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
