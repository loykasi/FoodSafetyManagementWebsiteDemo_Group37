using System.ComponentModel.DataAnnotations;
using WebAnToanVeSinhThucPhamDemo.Models.Blog;

namespace WebAnToanVeSinhThucPhamDemo.Areas.Blog.Models
{
    public class CreatePostModel : TinTuc
    {
        [Display(Name = "Chuyên mục")]
        public int[] IDChuyenMucs { get; set; }
    }
}
