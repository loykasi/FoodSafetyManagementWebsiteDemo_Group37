using System;
using System.Collections.Generic;

namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class TinTuc
{
    public int IdtinTuc { get; set; }

    public string? TieuDe { get; set; }

    public string? NoiDung { get; set; }

    public DateOnly? NgayDang { get; set; }

    public int? IdcanBo { get; set; }

    public virtual CanBo? IdcanBoNavigation { get; set; }
}
