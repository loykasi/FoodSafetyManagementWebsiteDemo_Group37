using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebAnToanVeSinhThucPhamDemo.Models;

namespace WebAnToanVeSinhThucPhamDemo.Controllers
{
    public class CoSoController : Controller
    {
        private readonly QlattpContext _context;

        public CoSoController(QlattpContext context)
        {
            _context = context;
        }

        // GET: CoSo
        public async Task<IActionResult> Index()
        {
            var coSoes = await _context.CoSos.ToListAsync();
            return View(coSoes);
        }

        // GET: CoSo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coSo = await _context.CoSos
                .FirstOrDefaultAsync(m => m.IdcoSo == id);
            if (coSo == null)
            {
                return NotFound();
            }

            return View(coSo);
        }

        // GET: CoSo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CoSo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdcoSo,IdchuCoSo,TenCoSo,DiaChi,IDPhuongXa,LoaiHinhKinhDoanh,SoGiayPhepKd,NgayCapGiayPhepKd,NgayCapCnattp,NgayHetHanCnattp,ChuCoSoId")] CoSo coSo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coSo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coSo);
        }

        // GET: CoSo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coSo = await _context.CoSos.FindAsync(id);
            if (coSo == null)
            {
                return NotFound();
            }
            return View(coSo);
        }

        // POST: CoSo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdcoSo,IdchuCoSo,TenCoSo,DiaChi,IDPhuongXa,LoaiHinhKinhDoanh,SoGiayPhepKd,NgayCapGiayPhepKd,NgayCapCnattp,NgayHetHanCnattp,ChuCoSoId")] CoSo coSo)
        {
            if (id != coSo.IdcoSo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coSo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoSoExists(coSo.IdcoSo))
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
            return View(coSo);
        }

        // GET: CoSo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coSo = await _context.CoSos
                .FirstOrDefaultAsync(m => m.IdcoSo == id);
            if (coSo == null)
            {
                return NotFound();
            }

            return View(coSo);
        }

        // POST: CoSo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coSo = await _context.CoSos.FindAsync(id);
            _context.CoSos.Remove(coSo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoSoExists(int id)
        {
            return _context.CoSos.Any(e => e.IdcoSo == id);
        }
    }
}
