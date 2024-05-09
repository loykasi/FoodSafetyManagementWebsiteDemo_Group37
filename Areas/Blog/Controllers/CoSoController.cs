using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebAnToanVeSinhThucPhamDemo.Data;
using WebAnToanVeSinhThucPhamDemo.Models;
using WebAnToanVeSinhThucPhamDemo.Utilities;
using App.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAnToanVeSinhThucPhamDemo.Areas.Blog.Models;
using WebAnToanVeSinhThucPhamDemo.Models.Blog;



namespace WebAnToanVeSinhThucPhamDemo.Areas.Blog.Controllers
{
    [Area("Blog")]
    [Route("member/blog/coso/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator + "," + RoleName.Member)]
    public class CoSoController : Controller
    {
        private readonly QlattpContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public CoSoController(QlattpContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            StatusMessage = string.Empty;
        }

        [TempData]
        public string StatusMessage { get; set; }

        // GET: Blog/CoSo/Index
        public async Task<IActionResult> Index(string searchString)
        {
            var phuongXaList = await _dbContext.PhuongXa.ToListAsync();
            var user = await _userManager.GetUserAsync(this.User);
            if (user == null)
            {
                // Xử lý trường hợp người dùng không tồn tại
                return NotFound();
            }

            IQueryable<CoSo> coSoQuery = _dbContext.CoSos;

            if (!string.IsNullOrEmpty(searchString))
            {
                coSoQuery = coSoQuery.Where(c =>
                    c.TenCoSo.Contains(searchString) ||
                    c.ChuCoSo.HoTen.Contains(searchString) ||
                    c.SoGiayPhepKd.Contains(searchString)
                );
            }

            // Nếu là admin, hiển thị tất cả các cơ sở và thông tin của chủ cơ sở
            if (User.IsInRole(RoleName.Administrator))
            {
                var allCoSoList = await coSoQuery.Include(c => c.ChuCoSo).ToListAsync();
                return View(allCoSoList);
            }
            else
            {
                // Nếu không phải admin, chỉ hiển thị các cơ sở mà người dùng hiện tại là chủ sở hữu
                coSoQuery = coSoQuery.Where(c => c.ChuCoSoId == user.Id);
                var myCoSoList = await coSoQuery.ToListAsync();
                return View(myCoSoList);
            }
        }





        // GET: Blog/Post/Create
        public async Task<IActionResult> CreateAsync()
        {
            var phuongxa = await _dbContext.PhuongXa.ToListAsync();

            ViewData["phuongxa"] = new MultiSelectList(phuongxa, "IDPhuongXa", "TenPhuongXa");

            return View();
        }

        // POST: Blog/Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenCoSo,DiaChi,LoaiHinhKinhDoanh,SoGiayPhepKd,NgayCapGiayPhepKd,NgayCapCnattp,NgayHetHanCnattp,IDPhuongXa")] CoSo post)
        {
            var phuongxa = await _dbContext.PhuongXa.ToListAsync();
            ViewData["phuongxa"] = new MultiSelectList(phuongxa, "IDPhuongXa", "TenPhuongXa");

            if (!ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(this.User);
                
                post.ChuCoSoId = user.Id;
                _dbContext.Add(post);

                await _dbContext.SaveChangesAsync();
                StatusMessage = "Vừa thêm cơ sở mới";
                return RedirectToAction(nameof(Index));
            }


            return View(post);
        }



        // GET: Blog/Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _dbContext.CoSos
                .Include(p => p.ChuCoSo)
                .FirstOrDefaultAsync(m => m.IdcoSo == id);
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
            var post = await _dbContext.CoSos.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            _dbContext.CoSos.Remove(post);
            await _dbContext.SaveChangesAsync();

            StatusMessage = "Bạn vừa xóa cơ sở: " + post.TenCoSo;

            return RedirectToAction(nameof(Index));
        }



        // GET: Blog/Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _dbContext.CoSos
                .Include(p => p.ChuCoSo)
                .FirstOrDefaultAsync(m => m.IdcoSo == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }


        // GET: Blog/CoSo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _dbContext.CoSos.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            // Lấy người dùng hiện tại đã đăng nhập
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                // Xử lý trường hợp người dùng không tồn tại
                return NotFound();
            }

            // Thiết lập ChuCoSoId cho cơ sở là Id của người dùng hiện tại
            post.ChuCoSoId = currentUser.Id;

            var phuongxa = await _dbContext.PhuongXa.ToListAsync();
            ViewBag.PhuongXaList = new SelectList(phuongxa, "IDPhuongXa", "TenPhuongXa");

            return View(post);
        }
        // POST: Blog/CoSo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdcoSo,TenCoSo,DiaChi,LoaiHinhKinhDoanh,SoGiayPhepKd,NgayCapGiayPhepKd,NgayCapCnattp,NgayHetHanCnattp,IDPhuongXa")] CoSo post)
        {
            if (id != post.IdcoSo)
            {
                return NotFound();
            }

            try
            {
                // Lấy người dùng hiện tại đã đăng nhập
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    // Xử lý trường hợp người dùng không tồn tại
                    return NotFound();
                }

                // Thiết lập ChuCoSoId cho cơ sở là Id của người dùng hiện tại
                post.ChuCoSoId = currentUser.Id;

                // Update cơ sở và lưu vào database
                _dbContext.Update(post);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _dbContext.CoSos.AnyAsync(p => p.IdcoSo == post.IdcoSo))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }






        private bool PostExists(int id)
        {
            return _dbContext.CoSos.Any(e => e.IdcoSo == id);
        }
    }
}
