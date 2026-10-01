using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HanBase.Models;

/// <summary>
/// Repository for 'kRSKangXi' unicode characters.
/// </summary>
[Table("RadicalStrokeCount")]
public class RadicalStrokeCount
{
    [Key]
    public long Id { get; set; } = 0L;

    [Required]
    public string Unicode { get; set; } = string.Empty;

    [Required]
    public int RadicalNumber { get; set; } = 0;

    [Required]
    public int StrokeCount { get; set; } = 0;
}
