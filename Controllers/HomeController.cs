using HanBase.Interfaces;
using HanBase.Models;
using HanBase.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HanBase.Controllers;

public class HomeController : Controller
{
    private readonly IReadingRepository _readingRepository;

    private readonly ITrainingRepository _trainingRepository;

    public HomeController(IReadingRepository readingRepository, ITrainingRepository trainingRepository)
    {
        _readingRepository = readingRepository;
        _trainingRepository = trainingRepository;
    }

    public ActionResult<ReadingViewModel> Index()
    {
        return View(_readingRepository.LoadReadingViewModel());
    }

    // POST: Home/Search
    [HttpPost]
    public async Task<ActionResult<ReadingViewModel>> Search(ReadingViewModel viewModel)
    {
        await _readingRepository.GetCharactersByCriteria(viewModel.CriteriaId, viewModel.Search, 1);

        return RedirectToAction(nameof(Index));
    }

    // GET: Home/SearchPager
    public async Task<IActionResult> SearchPager(int? criteria, string? search, int? page)
    {
        await _readingRepository.GetCharactersByCriteria(criteria ?? 0, search ?? string.Empty, page ?? 1);

        return RedirectToAction(nameof(Index));
    }

    // GET: Home/SearchDetails
    public async Task<IActionResult> SearchDetails(string? unicode)
    {
        await _readingRepository.GetCharacterByUnicode(unicode ?? string.Empty);

        return RedirectToAction(nameof(Index));
    }

    // GET: Home/Radical
    public async Task<IActionResult> Radical(int? radical)
    {
        await _readingRepository.GetCharactersByRadical(radical ?? 0);

        return RedirectToAction(nameof(Index));
    }

    // GET: Home/Strokes
    public void Strokes(int? strokes)
    {
        if (strokes == null || strokes < 0 || strokes > 50)
        {
            return;
        }

        var viewModel = _readingRepository.LoadReadingViewModel();
        viewModel.StrokeCount = strokes ?? 0;
    }

    // GET: Home/Charts
    public IActionResult Charts()
    {
        return View();
    }

    // GET: Home/Grammar
    public IActionResult Grammar()
    {
        return View();
    }

    // GET: Home/Training
    public ActionResult<TrainingViewModel> Training()
    {
        return View(_trainingRepository.LoadTrainingViewModel());
    }

    // POST: Home/Training
    [HttpPost]
    public async Task<IActionResult> Training(TrainingViewModel viewModel)
    {
        await _trainingRepository.GetCharactersByLanguage(viewModel.LanguageId, 1);

        return RedirectToAction(nameof(Training));
    }

    // GET: Home/TrainingPager
    public async Task<IActionResult> TrainingPager(int? language, int? page)
    {
        await _trainingRepository.GetCharactersByLanguage(language ?? 0, page ?? 1);

        return RedirectToAction(nameof(Training));
    }

    // GET: Home/TrainingDetails
    public async Task<IActionResult> TrainingDetails(string? unicode, int? position)
    {
        await _trainingRepository.GetCharacterByUnicode(unicode ?? string.Empty, position ?? 0, null);

        return RedirectToAction(nameof(Training));
    }

    // GET: Home/JumpPage
    public void JumpPage(int? page)
    {
        var viewModel = _trainingRepository.LoadTrainingViewModel();

        if (page == null || page <= 0 || page > viewModel.TotalPages)
        {
            return;
        }

        viewModel.PageJump = page ?? 1;
    }

    // GET: Home/Jump
    public async Task<IActionResult> Jump()
    {
        await _trainingRepository.JumpToPage();

        return RedirectToAction(nameof(Training));
    }

    // GET: Home/ExportPage
    public async Task<string> ExportPage()
    {
        return await _trainingRepository.ExportPage();
    }

    // GET: Home/Error
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
