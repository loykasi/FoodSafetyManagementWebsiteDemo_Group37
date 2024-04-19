using System;
using System.Collections.Generic;

namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class HoSoCapGiayChungNhan
{
    public int IdgiayChungNhan { get; set; }

    public int? IdcoSo { get; set; }

    public DateOnly? NgayDangKy { get; set; }

    public string? LoaiThucPham { get; set; }

    public string? HinhAnhMinhChung { get; set; }

    public int? TrangThai { get; set; }

    public virtual CoSo? IdcoSoNavigation { get; set; }
}
