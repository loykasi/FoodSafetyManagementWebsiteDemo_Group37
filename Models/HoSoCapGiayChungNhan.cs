using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class HoSoCapGiayChungNhan
{
    public int IdgiayChungNhan { get; set; }

    public int? IdcoSo { get; set; }
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
    [DataType(DataType.Date, ErrorMessage = "phai theo dinh dang dd/mm/yyyy")]
    public DateOnly? NgayDangKy { get; set; }

    public string? LoaiThucPham { get; set; }

    public string? HinhAnhMinhChung { get; set; }

    public int? TrangThai { get; set; }

    public virtual CoSo? IdcoSoNavigation { get; set; }
}
