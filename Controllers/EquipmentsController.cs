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
    public class EquipmentsController : Controller
    {
        private readonly PVEContext _context;

        public EquipmentsController(PVEContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Equipment.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipment
                .FirstOrDefaultAsync(m => m.ID == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Code,Name,Remark")] Equipment equipment)
        {
            if (!ModelState.IsValid) 
                return View(equipment);
            _context.Add(equipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipment.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }
            return View(equipment);
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Code,Name,Remark")] Equipment equipment)
        {
            if (id != equipment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentExists(equipment.ID))
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
            return View(equipment);
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipment
                .FirstOrDefaultAsync(m => m.ID == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipment = await _context.Equipment.FindAsync(id);
            _context.Equipment.Remove(equipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentExists(int id)
        {
            return _context.Equipment.Any(e => e.ID == id);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult ValidateCode(string code, int id)
        {
            if (_context.Equipment.Any(e => e.Code.Equals(code) && e.ID == id))
                return Json(true);//编辑状态
            return _context.Equipment.Any(e => e.Code.Equals(code)) ?
                Json($"设备编号 {code} 已经被占用") :
                Json(true);
        }
    }
}
