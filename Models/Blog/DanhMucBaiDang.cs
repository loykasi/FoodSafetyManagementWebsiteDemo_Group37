using System.ComponentModel.DataAnnotations.Schema;

namespace WebAnToanVeSinhThucPhamDemo.Models.Blog
{
    [Table("DanhMucBaiDang")]
    public class DanhMucBaiDang
    {
        public int IDBaiDang { set; get; }

        public int IDDanhMuc { set; get; }


        [ForeignKey("IDBaiDang")]
        public TinTuc TinTuc { set; get; }

        [ForeignKey("IDDanhMuc")]
        public DanhMuc DanhMuc { set; get; }
    }
}
