using WebAnToanVeSinhThucPhamDemo.Models;
using WebAnToanVeSinhThucPhamDemo.Models.Blog;

namespace WebAnToanVeSinhThucPhamDemo.Repository
{
    public class DanhMucRepository : IDanhMucRepository
    {
        private readonly QlattpContext _dbcontext;
        public DanhMucRepository(QlattpContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public DanhMuc Add(DanhMuc danhMuc)
        {
            _dbcontext.DanhMuc.Add(danhMuc);
            _dbcontext.SaveChanges();
            return danhMuc;
        }

        public DanhMuc Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DanhMuc> GetAllDanhMuc()
        {
            return _dbcontext.DanhMuc;
        }

        public DanhMuc Update(DanhMuc danhMuc)
        {
            _dbcontext.DanhMuc.Update(danhMuc);
            _dbcontext.SaveChanges();
            return danhMuc;
        }
        public DanhMuc GetDanhMuc(String Id)
        {
            return _dbcontext.DanhMuc.Find(Id); 
        }
    }
}
