using System;
using System.Collections.Generic;

namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class KeHoach
{
    public int IdkeHoach { get; set; }

    public DateTime? ThoiGianBatDau { get; set; }

    public int? DoanSo { get; set; }

    public string? Loai { get; set; }

    public virtual ICollection<ChiTietDoanThanhTra> ChiTietDoanThanhTras { get; set; } = new List<ChiTietDoanThanhTra>();

    public virtual ICollection<KeHoachCoSo> KeHoachCoSos { get; set; } = new List<KeHoachCoSo>();
}
