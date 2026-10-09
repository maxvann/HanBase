using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HanBase.Models;

[Table("MappedFontSeal")]
public class MappedFontSeal
{
    [Key]
    public long Id { get; set; } = 0L;

    [Required]
    public string Unicode { get; set; } = string.Empty;

    [Required]
    public string Ideogram { get; set; } = string.Empty;
}
