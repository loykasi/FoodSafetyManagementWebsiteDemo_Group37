using System;
using System.Collections.Generic;

namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class ChiTietDoanThanhTra
{
    public int IdkeHoach { get; set; }

    public string IdcanBo { get; set; } = null!;

    public string? ChucVu { get; set; }

    public virtual NguoiDung IdcanBoNavigation { get; set; } = null!;

    public virtual KeHoach IdkeHoachNavigation { get; set; } = null!;
}
