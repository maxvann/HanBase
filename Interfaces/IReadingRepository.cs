using HanBase.ViewModels;

namespace HanBase.Interfaces;

public interface IReadingRepository
{
    ReadingViewModel LoadReadingViewModel();

    Task<ReadingViewModel> GetCharacterByUnicode(string unicode);

    Task<ReadingViewModel> GetCharactersByCriteria(int criteriaId, string search, int page);

    Task<ReadingViewModel> GetCharactersByRadical(int radicalNumber);
}
