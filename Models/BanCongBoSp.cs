namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class BanCongBoSp
{
    public int IdbanCongBoSp { get; set; }

    public int? IdcoSo { get; set; }

    public DateOnly? NgayCongBo { get; set; }

    public string? TenSanPham { get; set; }


    public string? ThanhPhan { get; set; }

    public DateOnly? ThoiHanSuDung { get; set; }

    public string? CachDongGoiBaoBi { get; set; }

    public string? TenDiaChiSx { get; set; }

    public string? MauNhanSp { get; set; }

    public string? HinhAnhMinhChung { get; set; }

    public int? TrangThai { get; set; }

    public virtual CoSo? IdcoSoNavigation { get; set; }
}
