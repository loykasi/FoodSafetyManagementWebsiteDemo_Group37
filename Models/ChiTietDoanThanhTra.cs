using System;
using System.Collections.Generic;

namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class ChiTietDoanThanhTra
{
    public int IdkeHoach { get; set; }

    public int IdcanBo { get; set; }

    public string? ChucVu { get; set; }

    public virtual CanBo IdcanBoNavigation { get; set; } = null!;

    public virtual KeHoach IdkeHoachNavigation { get; set; } = null!;
}
