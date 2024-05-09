using App.Models;
using System.ComponentModel.DataAnnotations.Schema;
using WebAnToanVeSinhThucPhamDemo.Models;

public partial class ThongBaoThayDoi
{
    public int IdthongBao { get; set; }
    public int? IdcoSo { get; set; }
    public int? IdchuCoSoMoi { get; set; }
    public string? TenCoSoMoi { get; set; }
    public string? DiaChiMoi { get; set; }
    public int? TrangThai { get; set; }

    [ForeignKey("IdchuCoSoMoi")]
    public string ChuCoSoMoiId { get; set; }

    public virtual AppUser ChuCoSoMoi { get; set; }
    public virtual CoSo IdcoSoNavigation { get; set; }
}