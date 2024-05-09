using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAnToanVeSinhThucPhamDemo.Data;
using WebAnToanVeSinhThucPhamDemo.Models;
using WebAnToanVeSinhThucPhamDemo.Models.Contact;

namespace WebAnToanVeSinhThucPhamDemo.Areas.Contact.Controllers
{
    [Area("Contact")]
    [Authorize(Roles = RoleName.Administrator)]
    public class ContactController : Controller
    {
        private readonly QlattpContext _context;

        public ContactController(QlattpContext context)
        {
            _context = context;
        }

        [HttpGet("/admin/contact")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.LienHe.ToListAsync());
        }

        [HttpGet("/admin/contact/detail/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lienHe = await _context.LienHe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lienHe == null)
            {
                return NotFound();
            }

            return View(lienHe);
        }
        [TempData]
        public string StatusMessage { get; set; }
        [HttpGet("/contact/")]
        [AllowAnonymous]
        public IActionResult SendContact()
        {
            return View();
        }

        [HttpPost("/contact/")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendContact([Bind("HoTen,Email,NoiDung,Phone")] LienHe lienHe)
        {
            if (ModelState.IsValid)
            {
                lienHe.NgayGui = DateTime.Now;
                _context.Add(lienHe);
                await _context.SaveChangesAsync();
                StatusMessage = "Liên hệ đã được gửi";
                return RedirectToAction("Index", "Home");
            }
            return View(lienHe);
        }


        [HttpGet("/admin/contact/delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lienHe = await _context.LienHe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lienHe == null)
            {
                return NotFound();
            }

            return View(lienHe);
        }

        // POST: Contact/Contact/Delete/5
        [HttpPost("/admin/contact/delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lienHe = await _context.LienHe.FindAsync(id);
            if (lienHe != null)
            {
                _context.LienHe.Remove(lienHe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
