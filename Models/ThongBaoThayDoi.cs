using System;
using System.Collections.Generic;

namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class ThongBaoThayDoi
{
    public int IdthongBao { get; set; }

    public int? IdcoSo { get; set; }

    public int? IdchuCoSoMoi { get; set; }

    public string? TenCoSoMoi { get; set; }

    public string? DiaChiMoi { get; set; }

    public int? TrangThai { get; set; }

    public virtual ChuCoSo? IdchuCoSoMoiNavigation { get; set; }

    public virtual CoSo? IdcoSoNavigation { get; set; }
}
