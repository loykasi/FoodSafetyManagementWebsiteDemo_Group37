using Microsoft.AspNetCore.Mvc;
using WebAnToanVeSinhThucPhamDemo.Models;

namespace WebAnToanVeSinhThucPhamDemo.Controllers
{
    public class DangkyATTPController : Controller
    {
        private readonly IWebHostEnvironment _webHost;
        private DataContext _dataContext;

        public DangkyATTPController(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
            _dataContext = new DataContext();
        }

        public IActionResult Index()
        {
            AtvstpContext dbContext = new AtvstpContext();
            IList<QuanHuyen> listQuanHuyen = dbContext.QuanHuyens.ToList();
            IList<PhuongXa> listPhuongXa = dbContext.PhuongXas.ToList();
            ViewData["listQuanHuyen"] = listQuanHuyen;
            ViewData["listPhuongXa"] = listPhuongXa;
            return View();
        }

        [HttpPost] //Chạy cái action Insert của form ở view Index
        public ActionResult Insert(string tencoso,int phuongxa, string diachi, int? loaihinhkinhdoanh, string sogiayphep, DateOnly ngaycap, string loaithucpham, List<IFormFile> hinhanh)
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
				int maHoSo = _dataContext.insertGiayChungNhan_CoSo(tencoso, phuongxa, diachi, loaihinhkd, sogiayphep, ngaycap, loaithucpham, imageNames);

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

        public async Task<ActionResult> Test(List<IFormFile> file)
        {
            string uploadFile = Path.Combine(_webHost.WebRootPath,"HoSoDangKyATTP","1"); //1 la ma cua ho so
            if(!Directory.Exists(uploadFile))
            {
                Directory.CreateDirectory(uploadFile);
            }
            foreach(IFormFile f in file)
            {
                SaveImage(f,uploadFile);
            }
            ViewBag.message = "upload successfully";
            return View();
        }

        


		



	}
}
