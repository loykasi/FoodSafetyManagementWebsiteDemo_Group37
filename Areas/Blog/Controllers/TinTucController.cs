using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using WebAnToanVeSinhThucPhamDemo.Areas.Blog.Models;
using WebAnToanVeSinhThucPhamDemo.Data;
using WebAnToanVeSinhThucPhamDemo.Models;
using WebAnToanVeSinhThucPhamDemo.Models.Blog;
using WebAnToanVeSinhThucPhamDemo.Utilities;

namespace WebAnToanVeSinhThucPhamDemo.Areas.Blog.Controllers
{
    [Area("Blog")]
    [Route("admin/blog/tintuc/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator + "," + RoleName.Editor)]
    public class TinTucController : Controller
    {
        private readonly QlattpContext _dbcontext;
        private readonly UserManager<AppUser> _userManager;

        public TinTucController(QlattpContext context, UserManager<AppUser> userManager)
        {
            _dbcontext = context;
            _userManager = userManager;
            StatusMessage = string.Empty;
        }

        [TempData]
        public string StatusMessage { get; set; }
        // GET: TinTucController
        // GET: Blog/Post
        public async Task<IActionResult> Index([FromQuery(Name = "p")] int currentPage, int pagesize)
        {
            var posts = _dbcontext.TinTuc
                        .Include(p => p.CanBo)
                        .OrderByDescending(p => p.NgayCapNhat);

            int totalPosts = await posts.CountAsync();
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

            ViewBag.postIndex = (currentPage - 1) * pagesize;

            var postsInPage = await posts.Skip((currentPage - 1) * pagesize)
                             .Take(pagesize)
                             .Include(p => p.DanhMucBaiDangs)
                             .ThenInclude(pc => pc.DanhMuc)
                             .ToListAsync();

            return View(postsInPage);
        }

        // GET: Blog/Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _dbcontext.TinTuc
                .Include(p => p.CanBo)
                .FirstOrDefaultAsync(m => m.IDTinTuc == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }
        // GET: Blog/Post/Create
        public async Task<IActionResult> CreateAsync()
        {
            var categories = await _dbcontext.DanhMuc.ToListAsync();

            ViewData["categories"] = new MultiSelectList(categories, "Id", "TenDanhMuc");

            return View();
        }

        // POST: Blog/Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnhBia,TieuDe,MoTa,Slug,NoiDung,Published,IDChuyenMucs")] CreatePostModel post)
        {
            var categories = await _dbcontext.DanhMuc.ToListAsync();
            ViewData["categories"] = new MultiSelectList(categories, "Id", "TenDanhMuc");

            if (post.Slug == null)
            {
                post.Slug = AppUtilities.GenerateSlug(post.TieuDe);
            }

            if (await _dbcontext.TinTuc.AnyAsync(p => p.Slug == post.Slug))
            {
                ModelState.AddModelError("Slug", "Nhập chuỗi Url khác");
                return View(post);
            }



            if (!ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(this.User);
                post.NgayTao = post.NgayTao = DateTime.Now;
                post.NgayCapNhat = post.NgayCapNhat = DateTime.Now;
                post.IDCanBo = user.Id;
                _dbcontext.Add(post);

                if (post.IDChuyenMucs != null)
                {
                    foreach (var CateId in post.IDChuyenMucs)
                    {
                        _dbcontext.Add(new DanhMucBaiDang()
                        {
                            IDDanhMuc = CateId,
                            TinTuc = post
                        });
                    }
                }


                await _dbcontext.SaveChangesAsync();
                StatusMessage = "Vừa tạo bài viết mới";
                return RedirectToAction(nameof(Index));
            }


            return View(post);
        }
        // GET: Blog/Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var post = await _context.Posts.FindAsync(id);
            var post = await _dbcontext.TinTuc.Include(p => p.DanhMucBaiDangs).FirstOrDefaultAsync(p => p.IDTinTuc == id);
            if (post == null)
            {
                return NotFound();
            }

            var postEdit = new CreatePostModel()
            {
                IDTinTuc = post.IDTinTuc,
                AnhBia = post.AnhBia,
                TieuDe = post.TieuDe,
                MoTa = post.MoTa,
                NoiDung = post.NoiDung,
                Slug = post.Slug,
                Published = post.Published,
                IDChuyenMucs = post.DanhMucBaiDangs.Select(pc => pc.IDDanhMuc).ToArray()
            };

            var categories = await _dbcontext.DanhMuc.ToListAsync();
            ViewData["categories"] = new MultiSelectList(categories, "Id", "TenDanhMuc");

            return View(postEdit);
        }

        // POST: Blog/Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IDTinTuc,AnhBia,TieuDe,MoTa,Slug,NoiDung,Published,IDChuyenMucs")] CreatePostModel post)
        {
            if (id != post.IDTinTuc)
            {
                return NotFound();
            }
            var categories = await _dbcontext.DanhMuc.ToListAsync();
            ViewData["categories"] = new MultiSelectList(categories, "Id", "TenDanhMuc");


            if (post.Slug == null)
            {
                post.Slug = AppUtilities.GenerateSlug(post.TieuDe);
            }

            if (await _dbcontext.TinTuc.AnyAsync(p => p.Slug == post.Slug && p.IDTinTuc != id))
            {
                ModelState.AddModelError("Slug", "Nhập chuỗi Url khác");
                return View(post);
            }


            if (!ModelState.IsValid)
            {
                try
                {

                    var postUpdate = await _dbcontext.TinTuc.Include(p => p.DanhMucBaiDangs).FirstOrDefaultAsync(p => p.IDTinTuc == id);
                    if (postUpdate == null)
                    {
                        return NotFound();
                    }
                    postUpdate.AnhBia = post.AnhBia;
                    postUpdate.TieuDe = post.TieuDe;
                    postUpdate.MoTa = post.MoTa;
                    postUpdate.NoiDung = post.NoiDung;
                    postUpdate.Published = post.Published;
                    postUpdate.Slug = post.Slug;
                    postUpdate.NgayCapNhat = DateTime.Now;

                    // Update PostCategory
                    if (post.IDChuyenMucs == null) post.IDChuyenMucs = new int[] { };

                    var oldCateIds = postUpdate.DanhMucBaiDangs.Select(c => c.IDDanhMuc).ToArray();
                    var newCateIds = post.IDChuyenMucs;

                    var removeCatePosts = from postCate in postUpdate.DanhMucBaiDangs
                                          where (!newCateIds.Contains(postCate.IDDanhMuc))
                                          select postCate;
                    _dbcontext.DanhMucBaiDang.RemoveRange(removeCatePosts);

                    var addCateIds = from CateId in newCateIds
                                     where !oldCateIds.Contains(CateId)
                                     select CateId;

                    foreach (var CateId in addCateIds)
                    {
                        _dbcontext.DanhMucBaiDang.Add(new DanhMucBaiDang()
                        {
                            IDBaiDang = id,
                            IDDanhMuc = CateId
                        });
                    }

                    _dbcontext.Update(postUpdate);

                    await _dbcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.IDTinTuc))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                StatusMessage = "Vừa cập nhật bài viết";
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_dbcontext.Users, "Id", "Id", post.IDCanBo);
            return View(post);
        }

        // GET: Blog/Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _dbcontext.TinTuc
                .Include(p => p.CanBo)
                .FirstOrDefaultAsync(m => m.IDTinTuc == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Blog/Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _dbcontext.TinTuc.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            _dbcontext.TinTuc.Remove(post);
            await _dbcontext.SaveChangesAsync();

            StatusMessage = "Bạn vừa xóa bài viết: " + post.TieuDe;

            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _dbcontext.TinTuc.Any(e => e.IDTinTuc == id);
        }
    }
}


