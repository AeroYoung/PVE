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
    public class ErrorCodesController : BaseController
    {
        public ErrorCodesController(PVEContext context) : base(context)
        {
        }

        #region Index & Details

        [AllowAnonymous]
        public async Task<IActionResult> Index(int? foreignKey)
        {
            if (!GetPveData(foreignKey, out _))
                return RedirectToAction("Index", "PveDatas");

            var datas = from m in _context.ErrorCode
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

            var signal = await _context.ErrorCode
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
        public async Task<IActionResult> Create([Bind("Name,Code")] ErrorCode errorCode, int? foreignKey)
        {
            if (!ModelState.IsValid)
                return View(errorCode);

            if (!GetPveData(foreignKey, out var pveData))
                return RedirectToAction("Index", "PveDatas");

            errorCode.PveData = pveData;
            _context.Add(errorCode);
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

            var signal = await _context.ErrorCode.FindAsync(id);
            if (signal == null)
            {
                return NotFound();
            }
            return View(signal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Code,ID")] ErrorCode errorCode,
            int? foreignKey)
        {
            if (id != errorCode.ID)
            {
                return NotFound();
            }

            if (!GetPveData(foreignKey, out _))
                return RedirectToAction("Index", "PveDatas");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(errorCode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ErrorCodeExists(errorCode.ID))
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
            return View(errorCode);
        }

        // GET: ErrorCodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var errorCode = await _context.ErrorCode
                .FirstOrDefaultAsync(m => m.ID == id);
            if (errorCode == null)
            {
                return NotFound();
            }

            return View(errorCode);
        }

        // POST: ErrorCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var errorCode = await _context.ErrorCode.FindAsync(id);
            _context.ErrorCode.Remove(errorCode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ErrorCodeExists(int id)
        {
            return _context.ErrorCode.Any(e => e.ID == id);
        }
    }
}
