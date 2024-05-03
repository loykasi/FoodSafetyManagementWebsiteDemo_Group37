using System;
using System.Collections.Generic;

namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class QuanHuyen
{
    public int IdquanHuyen { get; set; }

    public string? TenQuanHuyen { get; set; }

    public virtual ICollection<PhuongXa> PhuongXas { get; set; } = new List<PhuongXa>();
}
