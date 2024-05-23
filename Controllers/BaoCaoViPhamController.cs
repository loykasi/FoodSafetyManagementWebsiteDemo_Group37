using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Text.Json.Serialization;
using WebAnToanVeSinhThucPhamDemo.Models;

namespace WebAnToanVeSinhThucPhamDemo.Controllers
{
    public class BaoCaoViPhamController : Controller
    {
        private readonly QlattpContext _context;
        private readonly IWebHostEnvironment _webHost;

        private int _currentPage = 1;
        private int _maxRowPerPage = 8;
        private int _totalPage = 0;
        private ViolationReportViewModel _viewModel = new ViolationReportViewModel();

        public BaoCaoViPhamController(QlattpContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        public ActionResult List()
        {
            var baoCaoViPhams = _context.BaoCaoViPhams.Include(b => b.IdcoSoNavigation)
                                                        .ThenInclude(c => c.PhuongXa)
                                                        .ThenInclude(p => p.QuanHuyen);
            return View(baoCaoViPhams);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baoCaoViPham = await _context.BaoCaoViPhams.Include(b => b.IdcoSoNavigation)
                                                        .ThenInclude(c => c.PhuongXa)
                                                        .ThenInclude(p => p.QuanHuyen)
                                                        .FirstOrDefaultAsync(b => b.IdbaoCao == id);
            return View(baoCaoViPham);
        }

        public ActionResult Index()
        {
            return View(GetViewModel(""));
        }

        [HttpGet]
        public JsonResult Search(int page = 1, string value = "")
        {
            Log("Seach");
            _currentPage = page;
            Log("" + _currentPage);
            return Json(GetViewModel(value));
        }

        [HttpPost]
        public ActionResult Index([Bind("HoTen,SDT,CCCD,NoiDung,IDCoSo")] ViolationReportViewModel baoCaoViPham, List<IFormFile> uploadFiles)
        {
            if (uploadFiles.Count == 0)
            {
                ModelState.AddModelError("uploadFiles", "Chọn hình ảnh minh chứng");
                return View(GetViewModel(""));
            }

            if (!ModelState.IsValid)
            {
                //var Errors = ModelState.Keys.Where(i => ModelState[i].Errors.Count > 0).Select(k => new KeyValuePair<string, string>(k, ModelState[k].Errors.First().ErrorMessage));
                //foreach (var item in Errors)
                //{
                //    Log(item.Value);
                //}
                TempData["AlertMessage"] = "Lỗi";
                return View(GetViewModel(""));
            }

            ViolationReportViewModel viewModel = new ViolationReportViewModel();
            int imageCount = 0;
            for (int i = 0; i < uploadFiles.Count; i++)
            {
                var file = uploadFiles[i];
                baoCaoViPham.HinhAnhMinhChung += imageCount != 0 ? "," + file.FileName : file.FileName;
                imageCount++;
            }

            Log($"{baoCaoViPham.HoTen} | {baoCaoViPham.SDT} | {baoCaoViPham.CCCD} | {baoCaoViPham.HinhAnhMinhChung} | {baoCaoViPham.IDCoSo}");

            var hoTenParam = new SqlParameter("@HoTen", baoCaoViPham.HoTen);
            var sdtParam = new SqlParameter("@SDT", baoCaoViPham.SDT);
            var cccdParam = new SqlParameter("@CCCD", baoCaoViPham.CCCD);
            var noiDungParam = new SqlParameter("@NoiDung", baoCaoViPham.NoiDung);
            var ngayBaoCaoParam = new SqlParameter("@NgayBaoCao", DateTime.Now);
            var hinhAnhMinhChungParam = new SqlParameter("@HinhAnhMinhChung", baoCaoViPham.HinhAnhMinhChung);
            var idCoSoParam = new SqlParameter("@IDCoSo", baoCaoViPham.IDCoSo);
            var newIdParam = new SqlParameter
            {
                ParameterName = "@NewId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            _context.Database.ExecuteSqlRaw("Execute pr_TaoBaoCaoViPham @HoTen, @SDT, @CCCD, @NoiDung, @NgayBaoCao, @HinhAnhMinhChung, @IDCoSo, @NewId out",
                                    hoTenParam, sdtParam, cccdParam, noiDungParam, ngayBaoCaoParam, hinhAnhMinhChungParam, idCoSoParam, newIdParam);

            int id = (int)newIdParam.Value;
            string uploadFolder = Path.Combine(_webHost.WebRootPath, "BaoCaoViPham", id.ToString());
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }
            foreach (IFormFile file in uploadFiles)
            {
                SaveImage(file, uploadFolder);
            }

            TempData["AlertMessage"] = "Gửi thành công";
            return RedirectToAction("Index");
        }

        private ViolationReportViewModel GetViewModel(string searchValue, bool isSearching = true)
        {
            if (isSearching)
            {
                _viewModel.CoSoes = _context.CoSos.Include(c => c.ChuCoSo).Include(c => c.PhuongXa).ThenInclude(p => p.QuanHuyen).ToList();

                if (!searchValue.ToLower().Equals(""))
                {
                    _viewModel.CoSoes = _viewModel.CoSoes.Where(s => s.TenCoSo.ToLower().Contains(searchValue.ToLower())).ToList();
                }

                _totalPage = _viewModel.CoSoes.Count;
            }
            // _viewModel.CoSoes = _viewModel.CoSoes.OrderBy(c => c.IdcoSo).Skip((_currentPage - 1) * _maxRowPerPage).Take(_maxRowPerPage).ToList();
            _viewModel.CurrentPage = _currentPage;
            _viewModel.TotalPage = ((_totalPage - 1) / _maxRowPerPage) + 1;
            return _viewModel;
        }

        private void SaveImage(IFormFile file, string path)
        {
            string fileName = Path.GetFileName(file.FileName);
            string fileSavePath = Path.Combine(path, fileName);
            using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }

        private void Log(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}
