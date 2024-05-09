using App.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class CoSo
{
    public int IdcoSo { get; set; }

    //public int? IdchuCoSo { get; set; }

    public string? TenCoSo { get; set; }

	public string? DiaChi { get; set; }

    [ForeignKey("IDPhuongXa")]
    public int? IDPhuongXa { get; set; }
    public virtual PhuongXa PhuongXa { get; set; }

    public string? LoaiHinhKinhDoanh { get; set; }

    public string? SoGiayPhepKd { get; set; }

    public DateOnly? NgayCapGiayPhepKd { get; set; }

    public DateOnly? NgayCapCnattp { get; set; }

    public DateOnly? NgayHetHanCnattp { get; set; }

    public virtual ICollection<BanCongBoSp> BanCongBoSps { get; set; } = new List<BanCongBoSp>();

    public virtual ICollection<BaoCaoViPham> BaoCaoViPhams { get; set; } = new List<BaoCaoViPham>();

    public virtual ICollection<HoSoCapGiayChungNhan> HoSoCapGiayChungNhans { get; set; } = new List<HoSoCapGiayChungNhan>();

    [ForeignKey("IdchuCoSo")]
    public string ChuCoSoId { get; set; }

    public virtual AppUser ChuCoSo { get; set; }

    public virtual ICollection<KeHoachCoSo> KeHoachCoSos { get; set; } = new List<KeHoachCoSo>();

    public virtual ICollection<ThongBaoThayDoi> ThongBaoThayDois { get; set; } = new List<ThongBaoThayDoi>();
}
