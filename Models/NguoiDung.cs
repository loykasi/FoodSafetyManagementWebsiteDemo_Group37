using System;
using System.Collections.Generic;

namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class NguoiDung
{
    public string Id { get; set; } = null!;

    public string? Cccd { get; set; }

    public string? DiaChiNha { get; set; }

    public DateTime? NgaySinh { get; set; }

    public string? TenDangNhap { get; set; }

    public string? TenDangNhapChuanHoa { get; set; }

    public string? Email { get; set; }

    public string? EmailChuanHoa { get; set; }

    public bool EmailDaXacNhan { get; set; }

    public string? MatKhauHash { get; set; }

    public string? DauVetBaoMat { get; set; }

    public string? DauVetDongBo { get; set; }

    public string? SoDienThoai { get; set; }

    public bool SoDienThoaiDaXacNhan { get; set; }

    public bool XacThucHaiYeuTo { get; set; }

    public DateTimeOffset? ThoiGianKhoaCuoiCung { get; set; }

    public bool DaKhoaTaiKhoan { get; set; }

    public int SoLanDangNhapThatBai { get; set; }

    public virtual ICollection<ChiTietDoanThanhTra> ChiTietDoanThanhTras { get; set; } = new List<ChiTietDoanThanhTra>();

    public virtual ICollection<CoSo> CoSos { get; set; } = new List<CoSo>();

    public virtual ICollection<ThongBaoThayDoi> ThongBaoThayDois { get; set; } = new List<ThongBaoThayDoi>();

    public virtual ICollection<TinTuc> TinTucs { get; set; } = new List<TinTuc>();

    public virtual ICollection<XacThucNguoiDung> XacThucNguoiDungs { get; set; } = new List<XacThucNguoiDung>();

    public virtual ICollection<VaiTro> VaiTros { get; set; } = new List<VaiTro>();
}
