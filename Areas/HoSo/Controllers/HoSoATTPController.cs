using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAnToanVeSinhThucPhamDemo.Data;
using WebAnToanVeSinhThucPhamDemo.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WebAnToanVeSinhThucPhamDemo.Areas.HoSo.Controllers
{
    [Area("HoSo")]
    [Route("hoso/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator + "," + RoleName.Editor)]
    public class HoSoATTPController : Controller
    {
        private readonly QlattpContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public HoSoATTPController(QlattpContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            StatusMessage = string.Empty;
        }

        [TempData]
        public string StatusMessage { get; set; }

        // GET: hoso/HoSoATTP/Index
        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int? phuongXaId, DateOnly? fromDate, DateOnly? toDate)
        {
            var user = await _userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return NotFound();
            }

            IQueryable<HoSoCapGiayChungNhan> hoSoQuery = _dbContext.HoSoCapGiayChungNhans
                .Include(h => h.IdcoSoNavigation)
                .ThenInclude(c => c.ChuCoSo);

            // Lọc theo tên cơ sở, tên chủ cơ sở, hoặc số giấy phép kinh doanh
            if (!string.IsNullOrEmpty(searchString))
            {
                hoSoQuery = hoSoQuery.Where(h =>
                    h.IdcoSoNavigation.TenCoSo.Contains(searchString) ||
                    h.IdcoSoNavigation.ChuCoSo.HoTen.Contains(searchString) ||
                    h.IdcoSoNavigation.SoGiayPhepKd.Contains(searchString)
                );
            }

            // Lọc theo phường xã
            if (phuongXaId != null && phuongXaId != 0)
            {
                hoSoQuery = hoSoQuery.Where(h => h.IdcoSoNavigation.IDPhuongXa == phuongXaId);
            }

            // Lọc theo thời gian từ fromDate đến toDate
            if (fromDate != null && toDate != null)
            {
                hoSoQuery = hoSoQuery.Where(h => h.NgayDangKy >= fromDate && h.NgayDangKy <= toDate);
            }

            var hoSoList = await hoSoQuery.ToListAsync();
            ViewBag.PhuongXaList = await _dbContext.PhuongXa.ToListAsync();
            return View(hoSoList);
        }




    }
}
