using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAnToanVeSinhThucPhamDemo.Data;
using WebAnToanVeSinhThucPhamDemo.Models;
using WebAnToanVeSinhThucPhamDemo.Models.Blog;

namespace WebAnToanVeSinhThucPhamDemo.Areas.Blog.Controllers
{
    [Area("Blog")]
    [Route("admin/blog/danhmuc/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator + "," + RoleName.Editor)]
    public class DanhMucController : Controller
    {
        private readonly QlattpContext _dbcontext;

        public DanhMucController(QlattpContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // GET: DanhMucController
        // GET: Blog/Category
        public async Task<IActionResult> Index()
        {
            var qr = (from c in _dbcontext.DanhMuc select c)
                     .Include(c => c.ParentCategory)
                     .Include(c => c.CategoryChildren);

            var categories = (await qr.ToListAsync())
                             .Where(c => c.ParentCategory == null)
                             .ToList();

            return View(categories);
        }


        // GET: Blog/Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _dbcontext.DanhMuc
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        private void CreateSelectItems(List<DanhMuc> source, List<DanhMuc> des, int level)
        {
            string prefix = string.Concat(Enumerable.Repeat("----", level));
            foreach (var category in source)
            {
                // category.Title = prefix + " " + category.Title;
                des.Add(new DanhMuc()
                {
                    Id = category.Id,
                    TenDanhMuc = prefix + " " + category.TenDanhMuc
                });
                if (category.CategoryChildren?.Count > 0)
                {
                    CreateSelectItems(category.CategoryChildren.ToList(), des, level + 1);
                }
            }
        }
        // GET: Blog/Category/Create
        public async Task<IActionResult> CreateAsync()
        {
            var qr = (from c in _dbcontext.DanhMuc select c)
                     .Include(c => c.ParentCategory)
                     .Include(c => c.CategoryChildren);

            var categories = (await qr.ToListAsync())
                             .Where(c => c.ParentCategory == null)
                             .ToList();
            categories.Insert(0, new DanhMuc()
            {
                Id = -1,
                TenDanhMuc = "Không có danh mục cha"
            });
            var items = new List<DanhMuc>();
            CreateSelectItems(categories, items, 0);
            var selectList = new SelectList(items, "Id", "TenDanhMuc");


            ViewData["ParentCategoryId"] = selectList;
            return View();
        }

        // POST: Blog/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenDanhMuc,NoiDung,Slug,ParentCategoryId")] DanhMuc category)
        {
            if (!ModelState.IsValid)
            {
                if (category.ParentCategoryId == -1) category.ParentCategoryId = null;
                _dbcontext.Add(category);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            var qr = (from c in _dbcontext.DanhMuc select c)
                     .Include(c => c.ParentCategory)
                     .Include(c => c.CategoryChildren);

            var categories = (await qr.ToListAsync())
                             .Where(c => c.ParentCategory == null)
                             .ToList();
            categories.Insert(0, new DanhMuc()
            {
                Id = -1,
                TenDanhMuc = "Không có danh mục cha"
            });
            var items = new List<DanhMuc>();
            CreateSelectItems(categories, items, 0);
            var selectList = new SelectList(items, "Id", "TenDanhMuc");


            ViewData["ParentCategoryId"] = selectList;
            return View(category);
        }
        // GET: Blog/Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _dbcontext.DanhMuc.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var qr = (from c in _dbcontext.DanhMuc select c)
                     .Include(c => c.ParentCategory)
                     .Include(c => c.CategoryChildren);

            var categories = (await qr.ToListAsync())
                             .Where(c => c.ParentCategory == null)
                             .ToList();
            categories.Insert(0, new DanhMuc()
            {
                Id = -1,
                TenDanhMuc = "Không có danh mục cha"
            });
            var items = new List<DanhMuc>();
            CreateSelectItems(categories, items, 0);
            var selectList = new SelectList(items, "Id", "TenDanhMuc");

            ViewData["ParentCategoryId"] = selectList;
            return View(category);
        }
        // POST: Blog/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenDanhMuc,NoiDung,Slug,ParentCategoryId")] DanhMuc category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            bool canUpdate = true;

            if (category.ParentCategoryId == category.Id)
            {
                ModelState.AddModelError(string.Empty, "Phải chọn danh mục cha khác");
                canUpdate = false;
            }

            // Kiem tra thiet lap muc cha phu hop
            if (canUpdate && category.ParentCategoryId != null)
            {
                var childCates =
                            (from c in _dbcontext.DanhMuc select c)
                            .Include(c => c.CategoryChildren)
                            .ToList()
                            .Where(c => c.ParentCategoryId == category.Id);


                // Func check Id 
                Func<List<DanhMuc>, bool> checkCateIds = null;
                checkCateIds = (cates) =>
                {
                    foreach (var cate in cates)
                    {
                        Console.WriteLine(cate.TenDanhMuc);
                        if (cate.Id == category.ParentCategoryId)
                        {
                            canUpdate = false;
                            ModelState.AddModelError(string.Empty, "Phải chọn danh mục cha khácXX");
                            return true;
                        }
                        if (cate.CategoryChildren != null)
                            return checkCateIds(cate.CategoryChildren.ToList());

                    }
                    return false;
                };
                // End Func 
                checkCateIds(childCates.ToList());
            }




            if (!ModelState.IsValid && canUpdate)
            {
                try
                {
                    if (category.ParentCategoryId == -1)
                        category.ParentCategoryId = null;

                    var dtc = _dbcontext.DanhMuc.FirstOrDefault(c => c.Id == id);
                    _dbcontext.Entry(dtc).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

                    _dbcontext.Update(category);
                    await _dbcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var qr = (from c in _dbcontext.DanhMuc select c)
                     .Include(c => c.ParentCategory)
                     .Include(c => c.CategoryChildren);

            var categories = (await qr.ToListAsync())
                             .Where(c => c.ParentCategory == null)
                             .ToList();

            categories.Insert(0, new DanhMuc()
            {
                Id = -1,
                TenDanhMuc = "Không có danh mục cha"
            });
            var items = new List<DanhMuc>();
            CreateSelectItems(categories, items, 0);
            var selectList = new SelectList(items, "Id", "TenDanhMuc");

            ViewData["ParentCategoryId"] = selectList;


            return View(category);
        }

        // GET: Blog/Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _dbcontext.DanhMuc
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Blog/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _dbcontext.DanhMuc
                           .Include(c => c.CategoryChildren)
                           .FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            foreach (var cCategory in category.CategoryChildren)
            {
                cCategory.ParentCategoryId = category.ParentCategoryId;
            }


            _dbcontext.DanhMuc.Remove(category);
            await _dbcontext.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _dbcontext.DanhMuc.Any(e => e.Id == id);
        }
    }
}

