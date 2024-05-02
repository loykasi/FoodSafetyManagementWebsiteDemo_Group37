using System;
using System.Collections.Generic;

namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class VaiTro
{
    public string Id { get; set; } = null!;

    public string? TenVaiTro { get; set; }

    public string? TenChuanHoa { get; set; }

    public string? DauVetDongBo { get; set; }

    public virtual ICollection<NguoiDung> NguoiDungs { get; set; } = new List<NguoiDung>();
}
