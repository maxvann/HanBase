using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HanBase.Models;

/// <summary>
/// Repository for the 'hun' pronunciation of a Korean character.
/// </summary>
[Table("KoreanHunReading")]
public class KoreanHunReading
{
    [Key]
    public long Id { get; set; } = 0L;

    [Required]
    public string CharacterNumber { get; set; } = string.Empty;

    [Required]
    public string RelatedUnicode { get; set; } = string.Empty;

    [Required]
    public string TranslatedReading { get; set; } = string.Empty;
}
