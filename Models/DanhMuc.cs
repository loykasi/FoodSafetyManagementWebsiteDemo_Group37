using System;
using System.Collections.Generic;

namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class DanhMuc
{
    public int Id { get; set; }

    public string TenDanhMuc { get; set; } = null!;

    public string NoiDung { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public int? IdDanhMucCha { get; set; }

    public virtual DanhMuc? IdDanhMucChaNavigation { get; set; }

    public virtual ICollection<DanhMuc> InverseIdDanhMucChaNavigation { get; set; } = new List<DanhMuc>();

    public virtual ICollection<TinTuc> IdtinTucs { get; set; } = new List<TinTuc>();
}
