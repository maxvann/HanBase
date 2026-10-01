using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HanBase.Models;

/// <summary>
/// Repository for variant unicode characters.
/// </summary>
[Table("Variant")]
public class Variant
{
    [Key]
    public long Id { get; set; } = 0L;

    [Required]
    public string Unicode { get; set; } = string.Empty;

    [Required]
    public string VariantType { get; set; } = string.Empty;

    [Required]
    public string RelatedUnicode { get; set; } = string.Empty;
}
