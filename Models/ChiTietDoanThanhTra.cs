using App.Models;
using System.ComponentModel.DataAnnotations.Schema;
using WebAnToanVeSinhThucPhamDemo.Models;

public partial class ChiTietDoanThanhTra
{
    public int IdkeHoach { get; set; }
    public int IdCanBo { get; set; }
    public string? ChucVu { get; set; }

    [ForeignKey("IdCanBo")]
    public string CanBoId { get; set; }

    public virtual AppUser CanBo { get; set; }
    public virtual KeHoach IdkeHoachNavigation { get; set; } = null!;
}