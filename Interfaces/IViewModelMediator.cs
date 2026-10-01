using HanBase.ViewModels;

namespace HanBase.Interfaces;

public interface IViewModelMediator
{
    ReadingViewModel GetReadingViewModel();

    void SetReadingViewModel(ReadingViewModel viewModel);

    TrainingViewModel GetTrainingViewModel();

    void SetTrainingViewModel(TrainingViewModel viewModel);
}
