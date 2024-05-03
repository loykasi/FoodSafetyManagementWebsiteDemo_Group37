using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAnToanVeSinhThucPhamDemo.Models;

namespace WebAnToanVeSinhThucPhamDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            ViewBag.TenNguoiDung = HttpContext.Session.GetString("tenNguoiDung");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(NguoiDung nguoiDung)
        {
            using (AtvstpContext db = new AtvstpContext())
            {
                var obj = db.NguoiDungs.Where(a => a.TenDangNhap.Equals(nguoiDung.TenDangNhap) && a.MatKhauHash.Equals(nguoiDung.MatKhauHash)).FirstOrDefault();
                if(obj != null)
                {
                    ISession session = HttpContext.Session;
                    session.SetString("idNguoiDung", obj.Id.ToString());
                    session.SetString("tenNguoiDung", obj.TenDangNhap);
                    return Redirect("Index");
                }
            }
            return Content("that bai");
        }
    }
}
