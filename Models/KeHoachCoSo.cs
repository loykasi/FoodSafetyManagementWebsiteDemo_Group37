using System;
using System.Collections.Generic;

namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class KeHoachCoSo
{
    public int IdkeHoachCoSo { get; set; }

    public int? IdkeHoach { get; set; }

    public int? IdcoSo { get; set; }

    public DateTime? ThoiGianKiemTra { get; set; }

    public DateOnly? NgayTao { get; set; }

    public string? KetLuan { get; set; }

    public string? KhacPhuc { get; set; }

    public string? YkienChuCoSo { get; set; }

    public virtual ICollection<ChiTietKetQua> ChiTietKetQuas { get; set; } = new List<ChiTietKetQua>();

    public virtual CoSo? IdcoSoNavigation { get; set; }

    public virtual KeHoach? IdkeHoachNavigation { get; set; }
}
