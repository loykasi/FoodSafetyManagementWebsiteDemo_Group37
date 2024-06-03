using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Security.Claims;
using System.Text.RegularExpressions;
using WebAnToanVeSinhThucPhamDemo.Data;
using WebAnToanVeSinhThucPhamDemo.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebAnToanVeSinhThucPhamDemo.Controllers
{
    [Authorize(Roles = RoleName.Editor + "," + RoleName.Member)]
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
        //Note
        public IActionResult DangKyGiayChungNhanMoi()
        {
            string tempPhuongXa = TempData["phuongXa"] == null ? "0" : TempData["phuongXa"].ToString();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Chọn quận huyện", Value = "0" });
            foreach (var i in _context.QuanHuyen)
            {
                li.Add(new SelectListItem { Text = i.TenQuanHuyen, Value = i.IDQuanHuyen.ToString() });
            }
            ViewData["quanhuyen"] = li;

            List<SelectListItem> li2 = new List<SelectListItem>();
            li2.Add(new SelectListItem { Text = "Chọn phường xã", Value = "0" });
            foreach (var i in _context.PhuongXa)
            {
                if (tempPhuongXa == i.IDPhuongXa.ToString())
                {
                    li2.Add(new SelectListItem { Text = i.TenPhuongXa, Value = i.IDPhuongXa.ToString(), Selected = true });
                }
                else
                {
                    li2.Add(new SelectListItem { Text = i.TenPhuongXa, Value = i.IDPhuongXa.ToString(), Selected = false });
                }
            }
            ViewData["quanhuyen"] = li;
            ViewData["phuongxa"] = li2;
            return View("Index");
        }

        //Note
        [HttpPost] //Chạy cái action Insert của form ở view Index
        [ActionName("Insert")]
        public ActionResult DangKyGiayChungNhanMoi(IFormCollection form)
        {
            bool isValid = true;
            string tencoso = form["tencoso"];
            string phuongxa = form["phuongxa"];
            string diachi = form["diachi"];
            string loaihinhkinhdoanh = form["loaihinhkinhdoanh"];
            string sogiayphep = form["sogiayphep"];
            DateOnly ngaycap;
            isValid = DateOnly.TryParse(form["ngaycap"].ToString(), out ngaycap);

            string loaithucpham = form["loaithucpham"];

            //độ dài 10 -> 100, ko chứ kí tự đặc biệt !@#
            string checkSpecialCharacter = @"^[^!@#]{10,100}$";
            string checkString = @"^[^!@#0-9]{10,100}$";
            //kí tự đầu là 0, 9 kí tự sau là số
            string checkPhone = @"^[0][1-9]{9}$";
            string checkSoDoanhNghiep = @"^[0-9]{10}$";
            if (!new Regex(checkSpecialCharacter).IsMatch(tencoso))

            {
                isValid = false;
                TempData["errorTenCoSo"] = "Tên không hợp lệ.";
            }
            if (!new Regex(checkSpecialCharacter).IsMatch(diachi))
            {
                isValid = false;
                TempData["errorDiaChi"] = "Địa chỉ không hợp lệ.";
            }
            if(loaihinhkinhdoanh == "0")
            {
                isValid = false;
                TempData["errorLoaiHinhKinhDoanh"] = "Hãy chọn loại hình kinh doanh.";
            }
            if(phuongxa == "0")
            {
                isValid = false;
                TempData["errorPhuongXa"] = "Hãy chọn phường xã.";
            }
            if (!new Regex(checkSoDoanhNghiep).IsMatch(sogiayphep))
            {
                isValid = false;
                TempData["errorSoGiayPhep"] = "Số giấy phép kinh doanh không hợp lệ";
            }
            if(ngaycap.Year < 1900)
            {
                TempData["errorNgayCap"] = "Ngày cấp giấy phép kinh doanh không hợp lệ";
            }
            if(!new Regex(checkString).IsMatch(loaithucpham))
            {
                isValid = false;
                TempData["errorLoaiThucPham"] = "Loại thực phẩm không hợp lệ";
            }
            if (form.Files.Count < 4)
            {
                isValid = false;
                TempData["errorHinhAnh"] = "Số lượng hình ảnh không đủ";
            }
            if(isValid == false)
            {
                TempData["tenCoSo"] = tencoso;
                TempData["diaChi"] = diachi;
                TempData["phuongXa"] = phuongxa;
                TempData["loaiHinhKinhDoanh"] = loaihinhkinhdoanh;
                TempData["soGiayPhep"] = sogiayphep;
                string test = ngaycap.ToString("yyyy-MM-dd");
                TempData["ngayCap"] = test;
                TempData["loaiThucPham"] = loaithucpham;
                //TempData["hinhAnh"] = form.Files;
                return RedirectToActionPermanent("DangKyGiayChungNhanMoi");
            }
            else
            {
                try
                {
                    String imageNames = "";
                    String loaihinhkd;
                    foreach (var file in form.Files)
                    {
                        imageNames += file.FileName + ",";
                    }
                    if (loaihinhkinhdoanh == "1")
                        loaihinhkd = "Cơ sở sản xuất, kinh doanh thực phẩm";
                    else
                        loaihinhkd = "Cơ sở kinh doanh dịch vụ ăn uống";
                    // chay lenh insert
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    int maHoSo = _dataContext.insertGiayChungNhan_CoSo(userId, tencoso, Convert.ToInt16(phuongxa), diachi, loaihinhkd, sogiayphep, ngaycap, loaithucpham, imageNames);

                    //luu file
                    string uploadFolder = Path.Combine(_webHost.WebRootPath, "HoSoDangKyATTP", maHoSo.ToString());
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }
                    foreach (var file in form.Files)
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
        //Note
        public JsonResult GetPhuongXa(string id)
        {
            if (id == "0")
            {
                return Json(new SelectList(_context.PhuongXa, "IDPhuongXa", "TenPhuongXa"));
            }
            return Json(new SelectList(_context.PhuongXa.Where(x=> x.IDQuanHuyen.ToString()  == id), "IDPhuongXa", "TenPhuongXa"));
        }

        //Code đăng ký lại giấy chứng nhận
        [HttpGet]
        public IActionResult DangKyLaiGiayChungNhan()
        {
            //lấy danh sách cơ sở theo idChuCoSo
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var listCoSo = _context.CoSos.Where(a => a.ChuCoSoId == userId);
            ViewData["listCoSo"] = new SelectList(listCoSo, "IdcoSo", "TenCoSo");
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
