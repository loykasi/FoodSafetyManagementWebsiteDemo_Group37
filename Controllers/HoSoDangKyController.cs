using Microsoft.AspNetCore.Mvc;
using WebAnToanVeSinhThucPhamDemo.Models;

namespace WebAnToanVeSinhThucPhamDemo.Controllers
{
    public class HoSoDangKyController : Controller
    {
        private readonly QlattpContext _dbcontext;

        public HoSoDangKyController(QlattpContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
