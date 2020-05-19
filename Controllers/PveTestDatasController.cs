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
    public class PveTestDatasController : BaseController
    {
        public PveTestDatasController(PVEContext context) : base(context)
        {
        }

        #region Index & Details

        [AllowAnonymous]
        public async Task<IActionResult> Index(int? foreignKey)
        {
            if (!GetPveData(foreignKey, out _))
                return RedirectToAction("Index", "PveDatas");

            var datas = from m in _context.PveTestData
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

            var signal = await _context.PveTestData
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
            [Bind("ID,PveDataID,BeginDate,BeginMan,BeginCompany,Type,Problem,Priors,ChargeMan,ChargeCompany,EndDate,State,FeedPercent,Remark,L19011")] PveTestData pveTestData, int? foreignKey)
        {
            if (!ModelState.IsValid)
                return View(pveTestData);

            if (!GetPveData(foreignKey, out var pveData))
                return RedirectToAction("Index", "PveDatas");

            pveTestData.PveData = pveData;
            _context.Add(pveTestData);
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

            var signal = await _context.PveTestData.FindAsync(id);
            if (signal == null)
            {
                return NotFound();
            }
            return View(signal);
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PveDataID,BeginDate,BeginMan,BeginCompany,Type,Problem,Priors,ChargeMan,ChargeCompany,EndDate,State,FeedPercent,Remark,L19011")] PveTestData pveTestData, int? foreignKey)
        {
            if (id != pveTestData.ID)
            {
                return NotFound();
            }

            if (!GetPveData(foreignKey, out _))
                return RedirectToAction("Index", "PveDatas");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pveTestData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PveTestDataExists(pveTestData.ID))
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
            return View(pveTestData);
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pveTestData = await _context.PveTestData
                .Include(p => p.PveData)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pveTestData == null)
            {
                return NotFound();
            }

            return View(pveTestData);
        }

        // POST: PveTestDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pveTestData = await _context.PveTestData.FindAsync(id);
            _context.PveTestData.Remove(pveTestData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PveTestDataExists(int id)
        {
            return _context.PveTestData.Any(e => e.ID == id);
        }
    }
}
