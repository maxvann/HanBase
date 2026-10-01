using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HanBase.Models;

/// <summary>
/// Repository for numeric value unicode characters.
/// </summary>
[Table("NumericValue")]
public class NumericValue
{
    [Key]
    public long Id { get; set; } = 0L;

    [Required]
    public string RelatedUnicode { get; set; } = string.Empty;

    [Required]
    public string NumericType { get; set; } = string.Empty;

    [Required]
    public long Numeric { get; set; } = 0L;
}
