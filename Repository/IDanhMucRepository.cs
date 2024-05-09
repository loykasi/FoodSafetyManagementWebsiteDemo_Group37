
using WebAnToanVeSinhThucPhamDemo.Models.Blog;

namespace WebAnToanVeSinhThucPhamDemo.Repository
{
    public interface IDanhMucRepository
    {
        DanhMuc Add (DanhMuc danhMuc);
        DanhMuc Update(DanhMuc danhMuc);
        DanhMuc Delete(String Id);
        IEnumerable<DanhMuc> GetAllDanhMuc();
    }
}
