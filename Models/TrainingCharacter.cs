using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HanBase.Models;

/// <summary>
/// Repository for variant unicode characters.
/// </summary>
[Table("TrainingCharacter")]
public class TrainingCharacter
{
    [Key]
    public long Id { get; set; } = 0L;

    [Required]
    public string CharacterNumber { get; set; } = string.Empty;

    [Required]
    public string CharacterUnicode { get; set; } = string.Empty;

    [Required]
    public string LanguageType { get; set; } = string.Empty;

    public string TrainingAttribute { get; set; } = string.Empty;
}
