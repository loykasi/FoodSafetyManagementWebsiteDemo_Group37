using System;
using System.Collections.Generic;

namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class ChucVu
{
    public int IdchucVu { get; set; }

    public string? TenChucVu { get; set; }

    public virtual ICollection<CanBo> CanBos { get; set; } = new List<CanBo>();
}
