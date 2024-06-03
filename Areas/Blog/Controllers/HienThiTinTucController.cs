using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAnToanVeSinhThucPhamDemo.Models;
using WebAnToanVeSinhThucPhamDemo.Models.Blog;

namespace WebAnToanVeSinhThucPhamDemo.Areas.Blog.Controllers
{
    [Area("Blog")]
    public class HienThiTinTucController : Controller
    {
        private readonly ILogger<HienThiTinTucController> _logger;
        private readonly QlattpContext _context;

        public HienThiTinTucController(ILogger<HienThiTinTucController> logger, QlattpContext context)
        {
            _logger = logger;
            _context = context;
        }


        [Route("/post/{categoryslug?}")]
        public IActionResult Index(string categoryslug, [FromQuery(Name = "p")] int currentPage, int pagesize)
        {
            var categories = GetCategories();
            ViewBag.categories = categories;
            ViewBag.categoryslug = categoryslug;

            DanhMuc category = null;

            if (!string.IsNullOrEmpty(categoryslug))
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                category = _context.DanhMuc.Where(c => c.Slug == categoryslug)
                                    .Include(c => c.CategoryChildren)
                                    .FirstOrDefault();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                if (category == null)
                {
                    return NotFound("Không thấy danh mục");
                }
            }
            var posts = _context.TinTuc
                .Include(p => p.CanBo)
                .Include(p => p.DanhMucBaiDangs)
                .ThenInclude(p => p.DanhMuc)
                .Where(p => p.Published == true) // Chỉ lấy bài đăng đã được xuất bản
                .OrderByDescending(p => p.NgayTao) // Sắp xếp theo ngày cập nhật mới nhất
                .AsQueryable();

            if (category != null)
            {
                var ids = new List<int>();
                category.ChildCategoryIDs(null, ids);
                ids.Add(category.Id);


                posts = posts.Where(p => p.DanhMucBaiDangs.Where(pc => ids.Contains(pc.IDDanhMuc)).Any());


            }

            int totalPosts = posts.Count();
            if (pagesize <= 0) pagesize = 10;
            int countPages = (int)Math.Ceiling((double)totalPosts / pagesize);

            if (currentPage > countPages) currentPage = countPages;
            if (currentPage < 1) currentPage = 1;

#pragma warning disable CS8603 // Possible null reference return.
            var pagingModel = new PagingModel()
            {
                countpages = countPages,
                currentpage = currentPage,
                generateUrl = (pageNumber) => Url.Action("Index", new
                {
                    p = pageNumber,
                    pagesize = pagesize
                })
            };
#pragma warning restore CS8603 // Possible null reference return.

            ViewBag.pagingModel = pagingModel;
            ViewBag.totalPosts = totalPosts;

            var postsInPage = posts.Skip((currentPage - 1) * pagesize)
                             .Take(pagesize);



            ViewBag.category = category;
            return View(postsInPage.ToList());
        }
        [HttpGet]
        [Route("post/{postslug}.html")]
        public IActionResult Detail(string postslug)
        {
            var categories = GetCategories();
            ViewBag.categories = categories;

            var post = _context.TinTuc.Where(p => p.Slug == postslug)
                               .Include(p => p.CanBo)
                               .Include(p => p.DanhMucBaiDangs)
                               .ThenInclude(pc => pc.DanhMuc)
                               .FirstOrDefault();

            if (post != null)
            {

                var existingPost = _context.TinTuc.Find(post.IDTinTuc);
                if (existingPost != null)
                {
                    existingPost.LuotXem++;
                    _context.SaveChanges();
                }
            }
            if (post == null)
            {
                return NotFound("Không thấy bài viết");
            }

            DanhMuc category = post.DanhMucBaiDangs.FirstOrDefault()?.DanhMuc;
            ViewBag.category = category;

            var otherPosts = _context.TinTuc.Where(p => p.DanhMucBaiDangs.Any(c => c.DanhMuc.Id == category.Id))
                                           .Where(p => p.IDTinTuc != post.IDTinTuc)
                                           .OrderByDescending(p => p.NgayTao)
                                           .Take(5);
            ViewBag.otherPosts = otherPosts;

            return View(post);
        }
        private List<DanhMuc> GetCategories()
        {
            var categories = _context.DanhMuc
                            .Include(c => c.CategoryChildren)
                            .AsEnumerable()
                            .Where(c => c.ParentCategory == null)
                            .ToList();
            return categories;
        }
    }
}
