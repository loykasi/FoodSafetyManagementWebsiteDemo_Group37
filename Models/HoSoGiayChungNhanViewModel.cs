using System.ComponentModel.DataAnnotations;

namespace WebAnToanVeSinhThucPhamDemo.Models
{
    public class HoSoGiayChungNhanViewModel
    {
        [Display(Name = "Mã Hồ Sơ")]
        public long IDGiayChungNhan { get; set; }

        [Display(Name = "Tên Cơ Sở")]
        public string TenCoSo { get; set; }

        [Display(Name = "Tên Chủ Cơ Sở")]
        public string HoTenChuCoSo { get; set; }

        [Display(Name = "Số Điện Thoại")]
        public string SDT { get; set; }

        [Display(Name = "Ngày đăng ký")]
        public Nullable<System.DateTime> NgayDangKy { get; set; }

        [Display(Name = "Trạng Thái")]
        public int TrangThai { get; set; }

        // Phương thức để lấy mô tả trạng thái
        public string GetTrangThaiDescription()
        {
            switch (TrangThai)
            {
                case 1:
                    return "Đã duyệt";
                case 2:
                    return "Chưa duyệt";
                default:
                    return "Không xác định";
            }
        }

    }
}

