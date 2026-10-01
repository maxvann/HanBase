using HanBase.ViewModels;

namespace HanBase.Interfaces;

public interface ITrainingRepository
{
    TrainingViewModel LoadTrainingViewModel();

    Task<TrainingViewModel> GetCharacterByUnicode(string unicode, int position, bool? standalone);

    Task<TrainingViewModel> GetCharactersByLanguage(int languageId, int page);

    Task<TrainingViewModel> JumpToPage();

    Task<string> ExportPage();
}
