using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mucsic.Models;
public class Composer
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Không được để trống")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    public string? Biography { get; set; }
    public string? ImageUrl { get; set; }
    [NotMapped] // Không tạo cột này trong Database
    public IFormFile? ImageFile { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public virtual ICollection<Song>? Songs { get; set; }
}