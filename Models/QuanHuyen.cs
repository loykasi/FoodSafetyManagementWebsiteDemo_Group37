using System.ComponentModel.DataAnnotations;

namespace WebAnToanVeSinhThucPhamDemo.Models
{
    public class QuanHuyen
    {
        [Key]
        public int IDQuanHuyen { get; set; }

        [Required]
        [StringLength(255)]
        public string TenQuanHuyen { get; set; }
    }
}
