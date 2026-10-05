using HanBase.Constants;
using HanBase.Entities;
using HanBase.Interfaces;
using HanBase.ViewModels;

namespace HanBase.Repositories;

public class ReadingRepository : IReadingRepository
{
    private readonly IViewModelMediator _viewModelMediator;

    private readonly ICharacterDetails _characterDetails;

    public ReadingRepository(IViewModelMediator viewModelMediator, ICharacterDetails characterDetails)
    {
        _viewModelMediator = viewModelMediator;
        _characterDetails = characterDetails;
    }

    #region "Properties"

    public ReadingViewModel LoadReadingViewModel()
    {
        return _viewModelMediator.GetReadingViewModel()!;
    }

    #endregion

    #region "Public Methods"

    public async Task<ReadingViewModel> GetCharacterByUnicode(string unicode)
    {
        var viewModel = LoadReadingViewModel();

        if (unicode == string.Empty || viewModel.Characters == null || viewModel.Characters.Count == 0 ||
            (viewModel.Character != null && viewModel.Character.Unicode == unicode))
        {
            return viewModel;
        }

        viewModel.Character = viewModel.Characters.FirstOrDefault(c => c.Unicode == unicode);

        var character = await _characterDetails.GetCharacter(unicode);

        if (viewModel.Character != null && character != null)
        {
            viewModel.Character.RadicalNumber = character.RadicalNumber;
            viewModel.Character.StrokeCount = character.StrokeCount;
        }

        viewModel.Readings = await _characterDetails.GetReadings(unicode);

        viewModel.Readings = _characterDetails.TransformReadings(viewModel.Readings);

        if (viewModel.Readings != null)
        {
            viewModel.Readings.KoreanHangulHun = await _characterDetails.GetKoreanHunReading(unicode);
        }

        viewModel.Numeric = await _characterDetails.GetNumericValue(unicode);

        if (viewModel.Character != null)
        {
            viewModel.CharacterRadicals = await _characterDetails.GetCharacterRadicals(viewModel.Character.RadicalNumber);

            viewModel.CharacterVariants = await _characterDetails.GetCharacterVariants(viewModel.Character.Unicode);
        }

        return viewModel;
    }

    public async Task<ReadingViewModel> GetCharactersByCriteria(int criteriaId, string search, int page)
    {
        var viewModel = LoadReadingViewModel();

        var characters = viewModel.Characters ?? [];

        if (search == string.Empty && viewModel.RadicalNumber > 0)
        {
            // Radical search
            characters = await _characterDetails.GetCharacters(viewModel.RadicalNumber, viewModel.StrokeCount);
            viewModel.Characters = characters;
            viewModel.CriteriaId = 0;
            viewModel.Search = string.Empty;
        }
        else
        {
            // Criteria search
            if ((search != null && search.Length > 0) && (viewModel.CriteriaId != criteriaId || viewModel.Search != search))
            {
                characters = await _characterDetails.SearchCharacters((SearchCriteria)criteriaId, search);
                viewModel.Characters = characters;
            }

            viewModel.CriteriaId = criteriaId;
            viewModel.RadicalNumber = 0;
            viewModel.Search = search ?? string.Empty;
        }

        viewModel.PaginatedCharacters = PaginatedList<CharacterDefinition>.Create(
            characters ?? [],
            page,
            Pages.ReadingsPageSize);

        return viewModel;
    }

    public async Task<ReadingViewModel> GetCharactersByRadical(int radicalNumber)
    {
        var viewModel = LoadReadingViewModel();

        viewModel.Characters = await _characterDetails.GetCharacters(radicalNumber, viewModel.StrokeCount);
        viewModel.RadicalNumber = radicalNumber;
        viewModel.CriteriaId = 0;
        viewModel.Search = string.Empty;

        viewModel.PaginatedCharacters = PaginatedList<CharacterDefinition>.Create(
            viewModel.Characters ?? [],
            1,
            Pages.ReadingsPageSize);

        return viewModel;
    }

    #endregion
}
