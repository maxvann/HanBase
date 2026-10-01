using System.ComponentModel.DataAnnotations;

namespace HanBase.Entities;

public class CharacterNumeric
{
    public string RelatedUnicode { get; set; } = string.Empty;

    public string NumericType { get; set; } = string.Empty;

    [Display(Name = "Numeric Value")]
    public long Numeric { get; set; } = -1L;
}
