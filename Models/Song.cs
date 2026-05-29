using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mucsic.Models;
public class Song
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Không được để trống")]
    [MinLength(5, ErrorMessage = "Ít nhất 5 ký tự")]
    public string Title { get; set; } = string.Empty;
    public string? Lyrics { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? Mp3Link { get; set; }
    public DateTime ReleaseDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    [Required]
    public int SingerId { get; set; }
    [ForeignKey("SingerId")]
    public virtual Singer? Singer { get; set; }
    [Required]
    public int ComposerId { get; set; }
    [ForeignKey("ComposerId")]
    public virtual Composer? Composer { get; set; }
    public int Status { get; set; } = 1;
}