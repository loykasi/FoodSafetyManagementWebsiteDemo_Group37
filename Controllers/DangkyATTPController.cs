using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAnToanVeSinhThucPhamDemo.Data;
using WebAnToanVeSinhThucPhamDemo.Models;

namespace WebAnToanVeSinhThucPhamDemo.Controllers
{
   
    public class DangkyATTPController : Controller
    {
        private readonly QlattpContext _context;
        private readonly IWebHostEnvironment _webHost;
        private DataContext _dataContext;

        public DangkyATTPController(IWebHostEnvironment webHost, QlattpContext context)
        {
            _context = context;
            _webHost = webHost;
            _dataContext = new DataContext();
        }

        public IActionResult DangKyGiayChungNhanMoi()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Chọn quận huyện", Value = "0" });
            foreach (var i in _context.QuanHuyen)
            {
                li.Add(new SelectListItem { Text = i.TenQuanHuyen, Value = i.IDQuanHuyen.ToString() });
            }
            ViewData["quanhuyen"] = li;
            return View("Index");
        }

        [HttpPost] //Chạy cái action Insert của form ở view Index
        [ActionName("Insert")]
        public ActionResult DangKyGiayChungNhanMoi(string tencoso, int phuongxa, string diachi, int? loaihinhkinhdoanh, string sogiayphep, DateOnly ngaycap, string loaithucpham, List<IFormFile> hinhanh)
        {
            try
            {
                String imageNames = "";
                String loaihinhkd;
                foreach (IFormFile file in hinhanh)
                {
                    imageNames += file.FileName + ",";
                }
                if (loaihinhkinhdoanh == 1)
                    loaihinhkd = "Cơ sở sản xuất, kinh doanh thực phẩm";
                else
                    loaihinhkd = "Cơ sở kinh doanh dịch vụ ăn uống";
                // chay lenh insert
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int maHoSo = _dataContext.insertGiayChungNhan_CoSo(userId, tencoso, phuongxa, diachi, loaihinhkd, sogiayphep, ngaycap, loaithucpham, imageNames);

                //luu file
                string uploadFolder = Path.Combine(_webHost.WebRootPath, "HoSoDangKyATTP", maHoSo.ToString());
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }
                foreach (IFormFile file in hinhanh)
                {
                    SaveImage(file, uploadFolder);
                }
                TempData["AlertMessage"] = "Gửi thành công";
                return RedirectToAction("DangKyGiayChungNhanMoi");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return Content("Đăng ký thất bại");
            }
        }

        //Function lưu hình ảnh
        public async void SaveImage(IFormFile file, string path)
        {
            string fileName = Path.GetFileName(file.FileName);
            string fileSavePath = Path.Combine(path, fileName);
            using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        public JsonResult GetPhuongXa(string id)
        {
            List<PhuongXa> listPhuongXa = _context.PhuongXa.Where(i => i.IDQuanHuyen.ToString() == id).ToList();
            List<SelectListItem> phuongxas = new List<SelectListItem>();
            foreach (var i in listPhuongXa)
            {
                phuongxas.Add(new SelectListItem { Text = i.TenPhuongXa, Value = i.IDPhuongXa.ToString() });
            }
            return Json(new SelectList(phuongxas, "Value", "Text"));
        }

        //Code đăng ký lại giấy chứng nhận
        [HttpGet]
        public IActionResult DangKyLaiGiayChungNhan()
        {
            //lấy danh sách cơ sở theo idChuCoSo
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var listCoSo = _context.CoSos.Where(a => a.ChuCoSoId == userId);
            List<SelectListItem> li = new List<SelectListItem>();
            foreach (var i in listCoSo)
            {
                li.Add(new SelectListItem { Text = i.TenCoSo, Value = i.IdcoSo.ToString() });
            }
            ViewData["listCoSo"] = li;
            return View("Index1");
        }

        [HttpPost]
        public IActionResult DangKyLaiGiayChungNhan(string coso, string loaithucpham, List<IFormFile> hinhanh)
        {
            try
            {
                
                String imageNames = "";
                foreach (IFormFile file in hinhanh)
                {
                    imageNames += file.FileName + ",";
                }
                //insert HoSoDangKyGiayChungNhan
                int maHoSo = _dataContext.insertGiayChungNhan(Int32.Parse(coso), loaithucpham, imageNames);

                //luu file
                string uploadFolder = Path.Combine(_webHost.WebRootPath, "HoSoDangKyATTP", maHoSo.ToString());
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }
                foreach (IFormFile file in hinhanh)
                {
                    SaveImage(file, uploadFolder);
                }
                return Content("Đăng ký thành công");

            }
            catch (Exception ex)
            {
                return Content("Đăng ký that bai");
            }
        }

        //Xem trang thai ho so
        [HttpGet]
        public IActionResult XemDanhSachHoSo()
        {
            var currentUserRole = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            IQueryable<HoSoCapGiayChungNhan> hoSoQuery;

            if (currentUserRole == "Admin")
            {
                hoSoQuery = _context.HoSoCapGiayChungNhans
                    .Include(h => h.IdcoSoNavigation)
                        .ThenInclude(c => c.ChuCoSo);
            }
            else
            {
                hoSoQuery = _context.HoSoCapGiayChungNhans
                    .Include(h => h.IdcoSoNavigation)
                        .ThenInclude(c => c.ChuCoSo)
                    .Where(hoSo => hoSo.IdcoSoNavigation.ChuCoSoId == userId);
            }

            var listHoSo = from hoSo in hoSoQuery
                           orderby hoSo.NgayDangKy descending
                           select new
                           {
                               tencoso = hoSo.IdcoSoNavigation.TenCoSo,
                               loaithucpham = hoSo.LoaiThucPham,
                               hinhanh = hoSo.HinhAnhMinhChung,
                               ngaydangky = hoSo.NgayDangKy,
                               trangthai = hoSo.TrangThai,
                               maHoSo = hoSo.IdgiayChungNhan,
                               tenChuCoSo = hoSo.IdcoSoNavigation.ChuCoSo.HoTen
                           };

            ViewBag.ListHoSo = listHoSo.ToList();
            return View("Index2");
        }

    }
}
