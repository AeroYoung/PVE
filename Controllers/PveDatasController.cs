using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PVE.Data;
using PVE.Models;

namespace PVE.Controllers
{
    public class PveDatasController : Controller
    {
        private readonly PVEContext _context;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public PveDatasController(PVEContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        #region Index

        [Authorize]
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter, 
            string filter,
            int pageNumber = 1,
            int pageSize = 50)
        {
            #region 分页|筛选|排序

            /*
             * 名为 currentFilter 的 ViewData 元素为视图提供当前筛选器字符串。
             * 此值必须包含在分页链接中，以便在分页过程中保持筛选器设置，并且在页面重新显示时必须将其还原到文本框中。
             *
             * 如果在分页过程中搜索字符串发生变化，则页面必须重置为 1，因为新的筛选器会导致显示不同的数据。
             * 在文本框中输入值并按下“提交”按钮时，搜索字符串将被更改。 在这种情况下，filter参数不为 NULL。
             */

            if (!string.IsNullOrWhiteSpace(filter))
                pageNumber = 1;
            else
                filter = currentFilter;

            ViewData["CurrentFilter"] = filter;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            var datas = from m in _context.PveData
                        select m;

            if (!string.IsNullOrEmpty(filter))
            {
                datas = datas.Where(s => s.VIN.Contains(filter));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    datas = datas.OrderByDescending(s => s.SerialNum);
                    break;
                case "Date":
                    datas = datas.OrderBy(s => s.ReleaseDate);
                    break;
                case "date_desc":
                    datas = datas.OrderByDescending(s => s.ReleaseDate);
                    break;
                default:
                    datas = datas.OrderBy(s => s.SerialNum);
                    break;
            }

            #endregion

            return View(await PaginatedList<PveData>.CreateAsync(datas, pageNumber, pageSize));
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pveData = await _context.PveData
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pveData == null)
            {
                return NotFound();
            }

            var folder = GetFolder(pveData.VIN);
            if (Directory.Exists(folder))
            {
                var files = Directory.GetFiles(folder);
                if (files.Length != 0)
                {
                    ViewBag.Files = files.Select(Path.GetFileName);
                }
            }

            return View(pveData);
        }

        #endregion

        #region Create

        [Authorize(Roles = Constants.AdministratorRole)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.AdministratorRole)]
        public async Task<IActionResult> Create(
            // 绑定字段防止过度发布
            [Bind("SerialNum,Producer,VehicleType,OBD,BOB,ReleaseDate,VehicleNum,VIN,TestContent,ProgressJ1,ProgressJ2D,ProgressJ2Z,ProgressJ2W,ProgressJ2H,ProgressJ2S,ProgressJ3,ContactCustomer,ContactMarket,ContactCATAC,Period,ContractType,Agreement,ProjectBid,FeeJ1,FeeJ2,FeeJ3,TaskForm,ReportDate,ReturnDate,FeeStatus,ProjectStatus,Remark")] PveData pveData)
        {
            if (!ModelState.IsValid) 
                return View(pveData);

            _context.Add(pveData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Edit

        // GET: PveDatas/Edit/5
        [Authorize(Roles = Constants.AdministratorRole)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pveData = await _context.PveData.FindAsync(id);
            if (pveData == null)
            {
                return NotFound();
            }
            return View(pveData);
        }

        // POST: PveDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.AdministratorRole)]
        public async Task<IActionResult> Edit(int id,
            [Bind("ID,SerialNum,Producer,VehicleType,OBD,BOB,ReleaseDate,VehicleNum,VIN,TestContent,ProgressJ1,ProgressJ2D,ProgressJ2Z,ProgressJ2W,ProgressJ2H,ProgressJ2S,ProgressJ3,ContactCustomer,ContactMarket,ContactCATAC,Period,ContractType,Agreement,ProjectBid,FeeJ1,FeeJ2,FeeJ3,TaskForm,ReportDate,ReturnDate,FeeStatus,ProjectStatus,Remark")] PveData pveData)
        {
            if (id != pveData.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pveData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PveDataExists(pveData.ID))
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
            return View(pveData);
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

            var pveData = await _context.PveData
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pveData == null)
            {
                return NotFound();
            }

            return View(pveData);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.AdministratorRole)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pveData = await _context.PveData.FindAsync(id);
            _context.PveData.Remove(pveData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Excel & File

        public IActionResult GetExcel()
        {
            var type = typeof(PveData);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Test");
                
                var excelData = package.GetAsByteArray();
                const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = $"PVE-所有数据-{DateTime.Now:yyyyMMdd}.xlsx";
                return File(excelData, contentType, fileName);
            }
        }

        [HttpPost]
        public IActionResult ImportExcel()
        {
            return View("Index");
        }

        private string GetFolder(string vin)
        {
            foreach (var rInvalidChar in Path.GetInvalidFileNameChars())
                vin = vin.Replace(rInvalidChar.ToString(), string.Empty);

            var webRootPath = _hostingEnvironment.WebRootPath;
            var folder = $"{webRootPath}\\Files\\{vin}\\PVEDATAS\\";
            return folder;
        }

        [HttpPost]
        public async Task<IActionResult> FileUpload(int id, string vin)
        {
            var files = Request.Form.Files;
            
            foreach (var formFile in files)
            {
                if (formFile.Length <= 0)
                    continue;

                var folder = GetFolder(vin);
                
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var filePath = $"{folder}{formFile.FileName}";
                await using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite,
                    FileShare.ReadWrite))
                {
                    await formFile.CopyToAsync(stream);
                }
            }

            return RedirectToAction(nameof(Details), new {id});
        }

        public IActionResult FileDownload(string vin, string fileName)
        {
            var folder = GetFolder(vin);
            var filePath = $"{folder}{fileName}";

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var stream = System.IO.File.OpenRead(filePath);
            var fileExt = Path.GetExtension(fileName);
            //获取文件的ContentType
            var provider = new FileExtensionContentTypeProvider();
            var meme = provider.Mappings[fileExt];
            return File(stream, meme, Path.GetFileName(filePath));
        }

        #endregion

        #region Other

        private bool PveDataExists(int id)
        {
            return _context.PveData.Any(e => e.ID == id);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult ValidateVIN(string vin, int id)
        {
            if (_context.PveData.Any(e => e.VIN.Equals(vin) && e.ID == id))
                return Json(true);//编辑状态
            return _context.PveData.Any(e => e.VIN.Equals(vin)) ? 
                Json($"VIN {vin} is already in use.") : 
                Json(true);
        }

        #endregion
    }
}
