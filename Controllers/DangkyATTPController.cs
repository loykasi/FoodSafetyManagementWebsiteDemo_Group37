using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Common;
using System.Net.Http.Headers;
using WebAnToanVeSinhThucPhamDemo.Models;

namespace WebAnToanVeSinhThucPhamDemo.Controllers
{
    public class DangkyATTPController : Controller
    {
        private readonly IWebHostEnvironment _webHost;
        private DataContext _dataContext;
		AtvstpContext dbContext = new AtvstpContext();

		public DangkyATTPController(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
            _dataContext = new DataContext();
        }

        public IActionResult Index()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text="Chọn quận huyện", Value="0"});
            foreach(var i in dbContext.QuanHuyens)
            {
                li.Add(new SelectListItem { Text = i.TenQuanHuyen, Value = i.IdquanHuyen.ToString() });
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

                string idchucoso = HttpContext.Session.GetString("idNguoiDung");

                // chay lenh insert
                int maHoSo = _dataContext.insertGiayChungNhan_CoSo(idchucoso,tencoso, phuongxa, diachi, loaihinhkd, sogiayphep, ngaycap, loaithucpham, imageNames);

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
            catch (Exception ex) {
                return Content(ex.Message);
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
            List<PhuongXa> listPhuongXa = dbContext.PhuongXas.Where(i => i.IdquanHuyen.ToString() == id).ToList();
            List<SelectListItem> phuongxas = new List<SelectListItem>();
        	foreach (var i in listPhuongXa)
            {
                phuongxas.Add(new SelectListItem { Text = i.TenPhuongXa, Value = i.IdphuongXa.ToString() });
            }
            return Json(new SelectList(phuongxas,"Value","Text"));
        }


        //Code đăng ký lại giấy chứng nhận
        [HttpGet]
        public IActionResult DangKyLaiGiayChungNhan()
        {
            //lấy danh sách cơ sở theo idChuCoSo
            string idChuCoSo = HttpContext.Session.GetString("idNguoiDung");
            var listCoSo = dbContext.CoSos.Where(a => a.IdchuCoSo == idChuCoSo);
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
                    imageNames = file.FileName + ",";
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
            catch(Exception ex)
            {
                return Content("Đăng ký that bai");
            }

        }





        


		



	}
}
