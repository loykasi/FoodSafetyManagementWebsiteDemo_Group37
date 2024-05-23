using System.ComponentModel.DataAnnotations;

namespace WebAnToanVeSinhThucPhamDemo.Models
{
    public class ViolationReportViewModel
    {
        [Required(ErrorMessage = "Cần nhập họ tên")]
        public string? HoTen { get; set; }

        [Required(ErrorMessage = "Cần nhập số điện thoại")]
        public string? SDT { get; set; }

        [Required(ErrorMessage = "Cần nhập số căn cước công dân")]
        public string? CCCD { get; set; }

        [Required(ErrorMessage = "Chọn cơ sở muốn báo cáo")]
        public int? IDCoSo { get; set; }

        public string? NoiDung { get; set; }
        public string? HinhAnhMinhChung { get; set; }
        public int? CurrentPage { get; set; }
        public int? TotalPage { get; set; }
        public List<CoSo>? CoSoes { get; set; }
    }
}