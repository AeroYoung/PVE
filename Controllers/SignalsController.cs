﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PVE.Data;
using PVE.Models;

namespace PVE.Controllers
{
    public class SignalsController : BaseController
    {
        public SignalsController(PVEContext context) : base(context)
        {
        }

        #region Index & Details

        [AllowAnonymous]
        public async Task<IActionResult> Index(int? foreignKey)
        {
            if (!GetPveData(foreignKey, out _))
                return RedirectToAction("Index", "PveDatas");

            var datas = from m in _context.Signal
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

            var signal = await _context.Signal
                .FirstOrDefaultAsync(m => m.ID == id);
            if (signal == null)
            {
                return NotFound();
            }
            return View(signal);
        }

        #endregion

        #region Create

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
            [Bind("PinNo,PinName,Func1,Func2,OBD")] Signal signal, int? foreignKey)
        {
            if (!ModelState.IsValid)
                return View(signal);

            if (!GetPveData(foreignKey, out var pveData))
                return RedirectToAction("Index", "PveDatas");

            signal.PveData = pveData;
            _context.Add(signal);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new {foreignKey = pveData.ID });
        }

        #endregion

        #region Edit

        [Authorize(Roles = Constants.AdministratorRole)]
        public async Task<IActionResult> Edit(int? id, int? foreignKey)
        {
            if (id == null)
                return NotFound();

            if (!GetPveData(foreignKey, out _))
                return RedirectToAction("Index", "PveDatas");

            var signal = await _context.Signal.FindAsync(id);
            if (signal == null)
            {
                return NotFound();
            }
            return View(signal);
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PinNo,PinName,Func1,Func2,OBD")] Signal signal, 
            int? foreignKey)
        {
            if (id != signal.ID)
            {
                return NotFound();
            }

            if (!GetPveData(foreignKey, out _))
                return RedirectToAction("Index", "PveDatas");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(signal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SignalExists(signal.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { foreignKey});
            }
            return View(signal);
        }

        #endregion

        #region Delete

        [Authorize(Roles = Constants.AdministratorRole)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signal = await _context.Signal
                .FirstOrDefaultAsync(m => m.ID == id);
            if (signal == null)
            {
                return NotFound();
            }

            return View(signal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var signal = await _context.Signal.FindAsync(id);
            _context.Signal.Remove(signal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SignalExists(int id)
        {
            return _context.Signal.Any(e => e.ID == id);
        }

        #endregion
    }
}
