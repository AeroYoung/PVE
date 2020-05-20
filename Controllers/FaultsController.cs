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
    public class FaultsController : BaseController
    {
        public FaultsController(PVEContext context) : base(context)
        {
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int? foreignKey,int? equipmentId)
        {
            GetPveData(foreignKey, out _);
            GetEquipment(equipmentId, out _);

            var datas = from m in _context.Fault
                select m;

            if (foreignKey != null)
                datas = datas.Where(s => s.PveDataID.Equals(foreignKey.Value));
            if (equipmentId != null)
                datas = datas.Where(s => s.EquipmentId.Equals(equipmentId.Value));
            datas = datas.Include(d => d.PveData).Include(d => d.Equipment);
            //.OrderByDescending(d => d.BeginTime)
            return View(await datas.OrderByDescending(d => d.BeginTime).ToListAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id, int? foreignKey, int? equipmentId)
        {
            if (id == null)
                return NotFound();

            GetPveData(foreignKey, out _);
            GetEquipment(equipmentId, out _);

            var signal = await _context.Fault
                .FirstOrDefaultAsync(m => m.ID == id);
            if (signal == null)
            {
                return NotFound();
            }
            return View(signal);
        }

        public IActionResult Create(int? foreignKey, int? equipmentId)
        {
            if (!GetPveData(foreignKey, out _))
                return RedirectToAction("Index", "PveDatas");

            GetEquipment(equipmentId, out _);

            ViewData["EquipmentId"] = new SelectList(_context.Equipment, "ID", "Describe");
            return View();
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ID,PveDataID,EquipmentId,Code,Describe,BeginTime,EndTime")] Fault fault,
            int? foreignKey, int? equipmentId)
        {
            if (ModelState.IsValid)
            {
                if (!GetPveData(foreignKey, out var pveData))
                    return RedirectToAction("Index", "PveDatas");

                fault.PveData = pveData;
                _context.Add(fault);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["EquipmentId"] = new SelectList(_context.Equipment, "ID", "Describe", fault.EquipmentId);
            
            return View(fault);
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        public async Task<IActionResult> Edit(int? id, int? foreignKey, int? equipmentId)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!GetPveData(foreignKey, out _))
                return RedirectToAction("Index", "PveDatas");

            if (!GetEquipment(equipmentId, out _))
                return RedirectToAction("Index", "PveDatas");

            var fault = await _context.Fault.FindAsync(id);
            if (fault == null)
            {
                return NotFound();
            }
            ViewData["EquipmentId"] = new SelectList(_context.Equipment, "ID", "Code", fault.EquipmentId);

            return View(fault);
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("ID,EquipmentId,Code,Describe,BeginTime,EndTime")] Fault fault, 
            int? foreignKey, int? equipmentId)
        {
            if (id != fault.ID)
            {
                return NotFound();
            }

            if (foreignKey==null || !GetPveData(foreignKey, out var pveData))
                return RedirectToAction("Index", "PveDatas");

            if (!GetEquipment(equipmentId, out _))
                return RedirectToAction("Index", "PveDatas");

            fault.PveData = pveData;
            fault.PveDataID = foreignKey.Value;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fault);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaultExists(fault.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { foreignKey, equipmentId });
            }
            ViewData["EquipmentId"] = new SelectList(_context.Equipment, "ID", "Code", fault.EquipmentId);

            return View(fault);
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fault = await _context.Fault
                .Include(f => f.Equipment)
                .Include(f => f.PveData)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (fault == null)
            {
                return NotFound();
            }

            return View(fault);
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fault = await _context.Fault.FindAsync(id);
            _context.Fault.Remove(fault);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaultExists(int id)
        {
            return _context.Fault.Any(e => e.ID == id);
        }
    }
}
