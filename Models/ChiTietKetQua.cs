using System;
using System.Collections.Generic;

namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class ChiTietKetQua
{
    public int IdkeHoachCoSo { get; set; }

    public int IdmucKt { get; set; }

    public bool? Dat { get; set; }

    public virtual KeHoachCoSo IdkeHoachCoSoNavigation { get; set; } = null!;

    public virtual MucKiemTra IdmucKtNavigation { get; set; } = null!;
}
