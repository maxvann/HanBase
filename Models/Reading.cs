using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HanBase.Models;

/// <summary>
/// Repository for the CJK readings of the unicode characters.
/// </summary>
[Table("Reading")]
public class Reading
{
    [Key]
    public long Id { get; set; } = 0L;

    [Required]
    public string LanguageType { get; set; } = string.Empty;

    [Required]
    public string RelatedUnicode { get; set; } = string.Empty;

    [Required]
    public string TranslatedReading { get; set; } = string.Empty;
}
