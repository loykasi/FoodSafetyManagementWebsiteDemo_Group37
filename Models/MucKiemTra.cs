using System;
using System.Collections.Generic;

namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class MucKiemTra
{
    public int IdmucKt { get; set; }

    public string? NoiDung { get; set; }

    public virtual ICollection<ChiTietKetQua> ChiTietKetQuas { get; set; } = new List<ChiTietKetQua>();
}
