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
        public async Task<IActionResult> Index(string searchString, int? phuongXaId, int? trangThai, DateOnly? ngayBatDau, DateOnly? ngayKetThuc)
        {
            // Lấy danh sách phường xã để hiển thị trong dropdown menu
            ViewBag.PhuongXaList = await _dbContext.PhuongXa.ToListAsync();
            ViewBag.SelectedNgayBatDau = ngayBatDau;
            ViewBag.SelectedNgayKetThuc = ngayKetThuc;
            // Tạo một biến lưu trạng thái phường xã đã chọn
            ViewBag.SelectedPhuongXaId = phuongXaId;

            // Tạo một biến lưu trạng thái đã chọn
            ViewBag.SelectedTrangThai = trangThai;
          
            // Lấy danh sách hồ sơ từ database
            IQueryable<HoSoCapGiayChungNhan> hoSoQuery = _dbContext.HoSoCapGiayChungNhans
                .Include(h => h.IdcoSoNavigation)
                .ThenInclude(c => c.ChuCoSo);
            // Lọc theo khoảng ngày đăng ký
            if (ngayBatDau != null)
            {
                if (ngayKetThuc != null)
                {
                    hoSoQuery = hoSoQuery.Where(h => h.NgayDangKy >= ngayBatDau && h.NgayDangKy <= ngayKetThuc);
                }
                else
                {
                    // Nếu chỉ chọn ngày bắt đầu mà không chọn ngày kết thúc,
                    // chỉ hiển thị kết quả cho những hồ sơ có ngày đăng ký bằng ngày bắt đầu
                    hoSoQuery = hoSoQuery.Where(h => h.NgayDangKy == ngayBatDau);
                }
            }
            // Lọc theo khoảng ngày đăng ký
            if (ngayKetThuc != null)
            {
                if (ngayBatDau != null)
                {
                    hoSoQuery = hoSoQuery.Where(h => h.NgayDangKy >= ngayBatDau && h.NgayDangKy <= ngayKetThuc);
                }
                else
                {
                    // Nếu chỉ chọn ngày kết thúc mà không chọn ngày bắt đầu,
                    // chỉ hiển thị kết quả cho những hồ sơ có ngày đăng ký bằng ngày kết thúc
                    hoSoQuery = hoSoQuery.Where(h => h.NgayDangKy == ngayKetThuc);
                }
            }
            // Tìm kiếm theo từ khóa
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

            

            // Lọc theo trạng thái
            if (trangThai != null)
            {
                hoSoQuery = hoSoQuery.Where(h => h.TrangThai == trangThai);
            }

            // Lấy danh sách hồ sơ đã lọc
            var hoSoList = await hoSoQuery.ToListAsync();

            return View(hoSoList);
        }

        // GET: hoso/HoSoATTP/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoSo = await _dbContext.HoSoCapGiayChungNhans
                .Include(h => h.IdcoSoNavigation)
                .ThenInclude(c => c.ChuCoSo)
                .FirstOrDefaultAsync(h => h.IdgiayChungNhan == id);

            if (hoSo == null)
            {
                return NotFound();
            }

            // Kiểm tra và lấy thông điệp từ TempData
            if (TempData["DuyetSuccess"] != null)
            {
                ViewBag.DuyetSuccess = TempData["DuyetSuccess"].ToString();
            }
            if (TempData["DuyetWarning"] != null)
            {
                ViewBag.DuyetWarning = TempData["DuyetWarning"].ToString();
            }

            var listHoSo = from h in _dbContext.HoSoCapGiayChungNhans
                           where h.IdcoSo == hoSo.IdcoSo && h.IdgiayChungNhan != id
                           orderby h.NgayDangKy descending
                           select new
                           {
                               tencoso = h.IdcoSoNavigation.TenCoSo,
                               loaithucpham = h.LoaiThucPham,
                               hinhanh = h.HinhAnhMinhChung,
                               ngaydangky = h.NgayDangKy,
                               trangthai = h.TrangThai == 1 ? "Đã duyệt" : "Chưa duyệt",
                               maHoSo = h.IdgiayChungNhan,
                               tenChuCoSo = h.IdcoSoNavigation.ChuCoSo.HoTen
                           };

            ViewBag.ListHoSo = await listHoSo.ToListAsync();

            return View(hoSo);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Duyet(int id)
        {
            try
            {
                var hoSo = await _dbContext.HoSoCapGiayChungNhans.FirstOrDefaultAsync(h => h.IdgiayChungNhan == id);
                if (hoSo == null)
                {
                    return NotFound();
                }

                // Kiểm tra trạng thái của hồ sơ
                if (hoSo.TrangThai == 1) // Giả sử 1 là trạng thái đã duyệt
                {
                    TempData["DuyetWarning"] = "Hồ sơ đã được duyệt trước đó";
                    return RedirectToAction(nameof(Details), new { id = id });
                }

                // Gọi procedure duyetGiayChungNhan với id của hồ sơ cần duyệt
                await _dbContext.Database.ExecuteSqlInterpolatedAsync($"EXEC duyetGiayChungNhan {id}");

                // Set TempData
                TempData["DuyetSuccess"] = "Đã duyệt hồ sơ thành công";

                // Cập nhật lại dữ liệu và trả về trang chi tiết hồ sơ
                return RedirectToAction(nameof(Details), new { id = id });
            }
            catch (Exception ex)
            {
                // Xử lý nếu có lỗi xảy ra
                // Ví dụ: log lỗi, hiển thị thông báo lỗi cho người dùng, ...
                return RedirectToAction(nameof(Index)); // Hoặc chuyển hướng đến một trang lỗi khác
            }
        }


    }
}

