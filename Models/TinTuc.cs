using System;
using System.Collections.Generic;

namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class TinTuc
{
    public int IdtinTuc { get; set; }

    public string TieuDe { get; set; } = null!;

    public string MoTa { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string NoiDung { get; set; } = null!;

    public bool Published { get; set; }

    public string IdcanBo { get; set; } = null!;

    public DateTime NgayTao { get; set; }

    public DateTime NgayCapNhat { get; set; }

    public virtual NguoiDung IdcanBoNavigation { get; set; } = null!;

    public virtual ICollection<DanhMuc> IddanhMucs { get; set; } = new List<DanhMuc>();
}
