using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAnToanVeSinhThucPhamDemo.Models.Contact
{
    public class LienHe
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        [Required(ErrorMessage = "Phải nhập {0}")]
        [Display(Name = "Họ và tên")]
        public string? HoTen { get; set; }

        [Required(ErrorMessage = "Phải nhập  {0}")]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Phải là địa chỉ email")]
        public string? Email { get; set; }

        public DateTime? NgayGui { get; set; }

        [Display(Name = "Nội dung")]
        public string? NoiDung { get; set; }

        [StringLength(50)]
        [Phone(ErrorMessage = "Phải là số diện thoại")]
        [Display(Name = "Số điện thoại")]
        public string? Phone { get; set; }
    }
}
