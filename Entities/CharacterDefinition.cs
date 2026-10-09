using System.ComponentModel.DataAnnotations;

namespace HanBase.Entities;

public class CharacterDefinition
{
    [Display(Name = "Unicode")]
    public string Unicode { get; set; } = string.Empty;

    [Display(Name = "Ideogram")]
    public string Ideogram { get; set; } = string.Empty;

    [Display(Name = "Variant Type")]
    public string VariantType { get; set; } = string.Empty;

    [Display(Name = "Translated Reading")]
    public string TranslatedReading { get; set; } = string.Empty;

    [Display(Name = "Radical Number")]
    public int RadicalNumber { get; set; } = 0;

    [Display(Name = "Stroke Count")]
    public int StrokeCount { get; set; } = 0;

    [Display(Name = "Position Number")]
    public int PositionNumber { get; set; } = 0;

    [Display(Name = "Is Mapped Brush")]
    public bool IsMappedBrush { get; set; } = false;

    [Display(Name = "Is Mapped Seal")]
    public bool IsMappedSeal { get; set; } = false;
}
/*

VariantType
===========

Property                    Introduced  Description
kSemanticVariant            	2.0     Ideographs with identical meanings (e.g. 兎 and 兔, both "rabbit")
kSpecializedSemanticVariant     2.0     Ideographs with overlapping meanings — identical only in specific contexts (e.g. 井 "well" vs 丼, which can mean "well" but also "a bowl of food")
kSimplifiedVariant              2.0     The simplified Chinese variant(s) for this ideograph
kTraditionalVariant             2.0     The traditional Chinese variant(s) for this ideograph
kZVariant                       2.0     Known z-variants — characters that should not be used interchangeably but are retained for round-trip compatibility with earlier encodings
kSpoofingVariant                13.0	Ideograph pairs that look similar (especially at small point sizes) but are not already z-variants or compatibility variants
kJapaneseNewVariant             18.0	The new Japanese form(s) (新字体) of this ideograph
kJapaneseOldVariant             18.0	The old Japanese form(s) (旧字体) of this ideograph

*/
