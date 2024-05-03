using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
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

        public IActionResult Index()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Chọn quận huyện", Value = "0" });
            foreach (var i in _context.QuanHuyen)
            {
                li.Add(new SelectListItem { Text = i.TenQuanHuyen, Value = i.IDQuanHuyen.ToString() });
            }
            ViewData["quanhuyen"] = li;
            return View();
        }

        [HttpPost] //Chạy cái action Insert của form ở view Index
        public ActionResult Insert(string tencoso, int phuongxa, string diachi, int? loaihinhkinhdoanh, string sogiayphep, DateOnly ngaycap, string loaithucpham, List<IFormFile> hinhanh)
        {
            try
            {
                String imageNames = "";
                String loaihinhkd;
                foreach (IFormFile file in hinhanh)
                {
                    imageNames = file.FileName + ",";
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
                return Content("Đăng ký thành công");
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

    }
}
