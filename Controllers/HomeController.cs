using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAnToanVeSinhThucPhamDemo.Models;
using WebAnToanVeSinhThucPhamDemo.Models.Blog;
namespace WebAnToanVeSinhThucPhamDemo.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly QlattpContext _context;
		public HomeController(ILogger<HomeController> logger, QlattpContext context)
		{
			_logger = logger;
			_context = context;
		}
		public IActionResult Index()
		{
			var categories = _context.DanhMuc.ToList();
			var postsByCategory = new Dictionary<DanhMuc, List<TinTuc>>();
			foreach (var category in categories)
			{
				var posts = _context.TinTuc
					.Where(p => p.Published && p.DanhMucBaiDangs.Any(pc => pc.IDDanhMuc == category.Id))
					.OrderByDescending(p => p.NgayTao)
					.Take(3)
					.ToList();
				if (posts.Count > 0)
				{
					postsByCategory.Add(category, posts);
				}
			}
			// Lấy danh sách 5 bài viết có lượt xem cao nhất
			var topViewedPosts = _context.TinTuc
				.Where(p => p.Published)
				.OrderByDescending(p => p.LuotXem)
				.Take(5)
				.ToList();
			ViewBag.TopViewedPosts = topViewedPosts;
			return View(postsByCategory);
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

		public IActionResult Guide()
		{
			return View();
		}
	}
}