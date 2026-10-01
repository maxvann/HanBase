using HanBase.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HanBase.ViewModels;

public class ReadingViewModel
{
    public PaginatedList<CharacterDefinition>? PaginatedCharacters { get; set; } = null;

    public List<CharacterDefinition>? Characters { get; set; } = null;

    public List<CharacterDefinition>? CharacterRadicals { get; set; } = null;

    public List<CharacterDefinition>? CharacterVariants { get; set; } = null;

    public CharacterDefinition? Character { get; set; } = null;

    public CharacterNumeric? Numeric { get; set; } = null;

    public CharacterReadings? Readings { get; set; } = null;

    public SelectList? Criteria { get; set; } = null;

    public string Search { get; set; } = string.Empty;

    [Display(Name = "Criteria")]
    public int CriteriaId { get; set; } = 0;

    public int RadicalNumber { get; set; } = 0;

    public int StrokeCount { get; set; } = 0;
}
