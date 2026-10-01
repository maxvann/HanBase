using HanBase.Interfaces;
using HanBase.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HanBase.Mediators;

public class ViewModelMediator : IViewModelMediator
{
    private ReadingViewModel? _readingViewModel = null;

    private TrainingViewModel? _trainingViewModel = null;

    private record LookupItem(int Id, string Description);

    public ViewModelMediator()
    {
        _readingViewModel ??= CreateReadingViewModel();
        _trainingViewModel ??= CreateTrainingViewModel();
    }

    public ReadingViewModel GetReadingViewModel()
    {
        return _readingViewModel ??= CreateReadingViewModel();
    }

    public void SetReadingViewModel(ReadingViewModel viewModel)
    {
        _readingViewModel = viewModel;
    }

    public TrainingViewModel GetTrainingViewModel()
    {
        return _trainingViewModel ??= CreateTrainingViewModel();
    }

    public void SetTrainingViewModel(TrainingViewModel viewModel)
    {
        _trainingViewModel = viewModel;
    }

    private static ReadingViewModel CreateReadingViewModel()
    {
        ReadingViewModel viewModel = new()
        {
            Criteria = new SelectList(new List<LookupItem> {
                new(0, "English Translation"),
                new(1, "Mandarin PinYin"),
                new(2, "Cantonese JyutPing"),
                new(3, "Japanese ON"),
                new(4, "Japanese Kun"),
                new(5, "Korean Yale"),
                new(6, "Unicode Code Point")
            }, "Id", "Description")
        };

        return viewModel;
    }

    private static TrainingViewModel CreateTrainingViewModel()
    {
        TrainingViewModel viewModel = new()
        {
            Languages = new SelectList(new List<LookupItem> {
                new(0, "Cantonese"),
                new(1, "Japanese"),
                new(2, "Korean"),
                new(3, "Mandarin"),
                new(4, "Mandarin100")
            }, "Id", "Description")
        };

        return viewModel;
    }
}
