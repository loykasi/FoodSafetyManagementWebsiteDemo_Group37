using System;
using System.Collections.Generic;

namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class XacThucNguoiDung
{
    public string NguoiDungId { get; set; } = null!;

    public string NhaCungCapDangNhap { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public string? GiaTri { get; set; }

    public virtual NguoiDung NguoiDung { get; set; } = null!;
}
