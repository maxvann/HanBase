using HanBase.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HanBase.ViewModels;

public class TrainingViewModel
{
    public string[]? Characters { get; set; } = null;

    public List<CharacterDefinition>? CharacterRadicals { get; set; } = null;

    public List<CharacterDefinition>? CharacterVariants { get; set; } = null;

    public CharacterDefinition? Character { get; set; } = null;

    public CharacterNumeric? Numeric { get; set; } = null;

    public CharacterReadings? Readings { get; set; } = null;

    public SelectList? Languages { get; set; } = null;

    public int LanguageId { get; set; } = 0;

    public int PageIndex { get; set; } = 0;

    public int TotalCharacters { get; set; } = 0;

    public int TotalPages { get; set; } = 0;

    public int CharacterNumber { get; set; } = 0;

    public int PageJump { get; set; } = 0;

    public bool HasPreviousPage { get; set; } = false;

    public bool HasNextPage { get; set; } = false;
}
