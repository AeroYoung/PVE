using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PVE.Data;
using PVE.Models;

namespace PVE.Controllers
{
    public class SignalsController : Controller
    {
        private readonly PVEContext _context;

        public SignalsController(PVEContext context)
        {
            _context = context;
        }

        #region Index

        [AllowAnonymous]
        public async Task<IActionResult> Index(int? foreignKey, string vin)
        {
            if (foreignKey == null)
                return RedirectToAction("Index", "PveDatas");

            ViewData[Constants.ViewDataPveDataId] = foreignKey;
            ViewData[Constants.ViewDataVIN] = vin;

            var datas = from m in _context.Signal
                select m;

            datas = datas.Where(s => s.PveData.ID.Equals(foreignKey.Value)).OrderBy(s => s.ID)
                .Include(d => d.PveData);

            return View(await datas.ToListAsync());
        }

        #endregion

        #region Details

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

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
        public IActionResult Create(int? foreignKey, string vin)
        {
            if(foreignKey==null)
                return RedirectToAction("Index", "PveDatas");

            ViewData[Constants.ViewDataPveDataId] = foreignKey;
            ViewData[Constants.ViewDataVIN] = vin;
            return View();
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("PveDataID,PinNo,PinName,Func1,Func2,OBD")] Signal signal, string vin)
        {
            if (!ModelState.IsValid) 
                return View(signal);
            _context.Add(signal);
            await _context.SaveChangesAsync();

            if (string.IsNullOrEmpty(vin))
                vin = "";

            return RedirectToAction(nameof(Index), new {foreignKey = signal.PveDataID, vin });
        }

        #endregion

        public async Task<IActionResult> Edit(int? id, int? foreignKey, string vin)
        {
            if (id == null)
                return NotFound();

            if (foreignKey == null)
                return RedirectToAction("Index", "PveDatas");

            ViewData[Constants.ViewDataPveDataId] = foreignKey;
            ViewData[Constants.ViewDataVIN] = vin;

            var signal = await _context.Signal.FindAsync(id);
            if (signal == null)
            {
                return NotFound();
            }
            return View(signal);
        }

        // POST: Signals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("PveDataID,ID,PinNo,PinName,Func1,Func2,OBD")] Signal signal
            , string vin)
        {
            if (id != signal.ID)
            {
                return NotFound();
            }

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
                return RedirectToAction(nameof(Index), new { foreignKey = signal.PveDataID, vin });
            }
            return View(signal);
        }

        // GET: Signals/Delete/5
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

        // POST: Signals/Delete/5
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

    }
}
