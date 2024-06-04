using App.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAnToanVeSinhThucPhamDemo.Models.Blog
{
    [Table("TinTuc")]
    public class TinTuc
    {
        [Key]
        public int IDTinTuc { set; get; }

        [Display(Name = "Ảnh bìa")]
        public string? AnhBia { get; set; }

        [Required(ErrorMessage = "Phải có tiêu đề bài viết")]
        [Display(Name = "Tiêu đề")]
        [StringLength(160, MinimumLength = 5, ErrorMessage = "{0} dài {1} đến {2}")]
        public string TieuDe { set; get; }
        [Display(Name = "Lượt xem")]
        public int LuotXem { get; set; } = 0;

        [Display(Name = "Mô tả ngắn")]
        public string MoTa { set; get; }

        [Display(Name = "Chuỗi định danh (url)", Prompt = "Nhập hoặc để trống tự phát sinh theo Title")]
        [StringLength(160, MinimumLength = 5, ErrorMessage = "{0} dài {1} đến {2}")]
        [RegularExpression(@"^[a-z0-9-]*$", ErrorMessage = "Chỉ dùng các ký tự [a-z0-9-]")]
        public string? Slug { set; get; }

        [Display(Name = "Nội dung")]
        public string NoiDung { set; get; }

        [Display(Name = "Xuất bản")]
        public bool Published { set; get; }

        public List<DanhMucBaiDang> DanhMucBaiDangs { get; set; }

        // [Required]
        [Display(Name = "Tác giả")]
        public string IDCanBo { set; get; }
        [ForeignKey("IDCanBo")]
        [Display(Name = "Tác giả")]
        public AppUser CanBo { set; get; }



        [Display(Name = "Ngày tạo")]
        public DateTime NgayTao { set; get; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime NgayCapNhat { set; get; }
    }
}
