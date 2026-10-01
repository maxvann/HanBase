using System.ComponentModel.DataAnnotations;

namespace HanBase.Entities;

public class CharacterRadical
{
    [Display(Name = "Number")]
    public int Number { get; set; } = 0;

    [Display(Name = "Unicode")]
    public string Unicode { get; set; } = string.Empty;

    [Display(Name = "Ideogram")]
    public string Ideogram { get; set; } = string.Empty;

    [Display(Name = "Count")]
    public int RadicalCount { get; set; } = 0;
}
