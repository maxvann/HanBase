using System.ComponentModel.DataAnnotations;

namespace HanBase.Entities;

public class CharacterReadings
{
    [Display(Name = "English Definition")]
    public string EnglishDefinition { get; set; } = string.Empty;

    [Display(Name = "Mandarin Chinese")]
    public string MandarinPinYin { get; set; } = string.Empty;

    [Display(Name = "Cantonese Chinese")]
    public string CantoneseJyutPing { get; set; } = string.Empty;

    [Display(Name = "Japanese ON")]
    public string JapaneseON { get; set; } = string.Empty;

    [Display(Name = "Japanese Kun")]
    public string JapaneseKun { get; set; } = string.Empty;

    [Display(Name = "Korean Yale")]
    public string KoreanYale { get; set; } = string.Empty;

    [Display(Name = "Korean Hangul UM")]
    public string KoreanHangul { get; set; } = string.Empty;

    [Display(Name = "Korean Hangul Hun")]
    public string KoreanHangulHun { get; set; } = string.Empty;
}
