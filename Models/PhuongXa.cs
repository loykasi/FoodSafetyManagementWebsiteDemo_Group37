
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAnToanVeSinhThucPhamDemo.Models
{
    public class PhuongXa
    {
        [Key]
        public int IDPhuongXa { get; set; }

        [Required]
        [StringLength(255)]
        public string TenPhuongXa { get; set; }

        [ForeignKey("QuanHuyen")]
        public int IDQuanHuyen { get; set; }

        public virtual QuanHuyen QuanHuyen { get; set; }
    }
}
