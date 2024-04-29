namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class CanBo
{
    public int IdcanBo { get; set; }

    public string? HoTen { get; set; }

    public string? Cccd { get; set; }

    public string? MatKhau { get; set; }

    public int? IdchucVu { get; set; }

    public virtual ICollection<ChiTietDoanThanhTra> ChiTietDoanThanhTras { get; set; } = new List<ChiTietDoanThanhTra>();

    public virtual ChucVu? IdchucVuNavigation { get; set; }

}
