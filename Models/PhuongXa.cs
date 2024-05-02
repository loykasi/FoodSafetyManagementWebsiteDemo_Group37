using System;
using System.Collections.Generic;

namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class PhuongXa
{
    public int IdphuongXa { get; set; }

    public string? TenPhuongXa { get; set; }

    public int? IdquanHuyen { get; set; }

    public virtual QuanHuyen? IdquanHuyenNavigation { get; set; }
}
