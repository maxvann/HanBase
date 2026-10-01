using HanBase.Constants;
using HanBase.Interfaces;
using HanBase.ViewModels;
using System.Text;

namespace HanBase.Repositories;

public class TrainingRepository : ITrainingRepository
{
    private readonly IViewModelMediator _viewModelMediator;

    private readonly ICharacterDetails _characterDetails;

    private readonly IWebHostEnvironment _env;

    public TrainingRepository(IWebHostEnvironment env, IViewModelMediator viewModelMediator, ICharacterDetails characterDetails)
    {
        _env = env;
        _viewModelMediator = viewModelMediator;
        _characterDetails = characterDetails;
    }

    #region "Properties"

    public TrainingViewModel LoadTrainingViewModel()
    {
        return _viewModelMediator.GetTrainingViewModel()!;
    }

    #endregion

    #region "Public Methods"

    public async Task<TrainingViewModel> GetCharacterByUnicode(string unicode, int position, bool? standalone)
    {
        var viewModel = new TrainingViewModel();

        if (standalone != null && standalone == true)
        {
            if (unicode == string.Empty)
            {
                return viewModel;
            }
        }
        else
        {
            viewModel = LoadTrainingViewModel();

            if (unicode == string.Empty || viewModel.Characters == null || viewModel.Characters.Length == 0 ||
                (viewModel.Character != null && viewModel.Character.Unicode == unicode))
            {
                return viewModel;
            }
        }

        viewModel.Character = await _characterDetails.GetCharacter(unicode);

        viewModel.Readings = await _characterDetails.GetReadings(unicode);

        viewModel.Readings = _characterDetails.TransformReadings(viewModel.Readings);

        if (viewModel.Readings != null)
        {
            viewModel.Readings.KoreanHangulHun = await _characterDetails.GetKoreanHunReading(unicode);
        }

        viewModel.Numeric = await _characterDetails.GetNumericValue(unicode);

        if (viewModel.Character != null)
        {
            viewModel.Character.PositionNumber = position;

            viewModel.CharacterRadicals = await _characterDetails.GetCharacterRadicals(viewModel.Character.RadicalNumber);

            viewModel.CharacterVariants = await _characterDetails.GetCharacterVariants(viewModel.Character.Unicode);
        }

        return viewModel;
    }

    public async Task<TrainingViewModel> GetCharactersByLanguage(int languageId, int page)
    {
        var viewModel = LoadTrainingViewModel();

        string characterTypeChar = GetCharacterChar((CharacterType)languageId);

        var totalCharacters = await _characterDetails.GetTrainingCharactersCount(characterTypeChar);

        var characters = await _characterDetails.GetTrainingCharacters(characterTypeChar, page, Pages.TrainingPageSize);

        viewModel.CharacterNumber = ((page - 1) * Pages.TrainingPageSize) + 1;
        viewModel.Characters = characters;
        viewModel.LanguageId = languageId;
        viewModel.PageIndex = page;
        viewModel.TotalCharacters = totalCharacters;
        viewModel.TotalPages = (int)Math.Ceiling(totalCharacters / (double)Pages.TrainingPageSize);
        viewModel.HasPreviousPage = viewModel.PageIndex > 1;
        viewModel.HasNextPage = viewModel.PageIndex < viewModel.TotalPages;

        return viewModel;
    }

    public async Task<TrainingViewModel> JumpToPage()
    {
        var viewModel = LoadTrainingViewModel();

        if (viewModel.PageJump <= 0)
        {
            viewModel.PageJump = 1;
        }

        if (viewModel.PageJump > viewModel.TotalPages)
        {
            viewModel.PageJump = viewModel.TotalPages;
        }

        await GetCharactersByLanguage(viewModel.LanguageId, viewModel.PageJump);

        return viewModel;
    }

    public async Task<string> ExportPage()
    {
        var viewModel = LoadTrainingViewModel();

        if (viewModel.Characters == null || viewModel.Characters.Length == 0 || viewModel.TotalPages == 0 || viewModel.PageIndex == 0)
        {
            return string.Empty;
        }

        var html = new StringBuilder();

        var characterNumber = viewModel.CharacterNumber;

        html.Append("<!DOCTYPE html>");
        html.Append("<html lang=\"en\">");
        html.Append("<head>");
        html.Append("   <meta charset=\"utf-8\" />");
        html.Append("   <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />");
        html.Append("   <meta name=\"description\" content=\"HanBase Chinese Character Dictionary\" />");
        html.Append("   <meta name=\"author\" content=\"HanBase Chinese Character Dictionary\" />");
        html.Append("   <title>HanBase</title>");
        html.Append("   <style>");
        html.Append("       body {");
        html.Append("           background-color: #ffffff;");
        html.Append("           color: #14202a;");
        html.Append("           font-family: Arial, Helvetica, sans-serif, system-ui;");
        html.Append("           font-size: medium;");
        html.Append("       }");
        html.Append("       .bold {");
        html.Append("           font-style: bold;");
        html.Append("           font-weight: 700;");
        html.Append("       }");
        html.Append("       .large {");
        html.Append("           font-size: x-large;");
        html.Append("       }");
        html.Append("       .small {");
        html.Append("           font-size: small;");
        html.Append("       }");
        html.Append("   </style>");
        html.Append("</head>");
        html.Append("<body>");

        html.Append("<h3>");
        html.Append(Enum.GetName(typeof(CharacterType), viewModel.LanguageId));
        html.Append("&nbsp;Listing for Page&nbsp;" + viewModel.PageIndex.ToString());
        html.Append("</h3>");

        foreach (string unicode in viewModel.Characters)
        {
            var details = await GetCharacterByUnicode(unicode, 0, true);

            if (details != null && details.Character != null && details.Readings != null)
            {
                html.Append("<div>");
                html.Append("<span class=\"bold\">&nbsp;" + characterNumber++ + "</span>");
                html.Append("<span class=\"large\">&nbsp;" + details.Character.Ideogram + "</span>");
                html.Append("<span class=\"small\">&nbsp;" + details.Character.Unicode + "</span>");
                html.Append("<span>&nbsp;(E)&nbsp;" + details.Readings.EnglishDefinition + "</span>");
                html.Append("<span>&nbsp;(M)&nbsp;" + details.Readings.MandarinPinYin + "</span>");
                html.Append("<span>&nbsp;(C)&nbsp;" + details.Readings.CantoneseJyutPing + "</span>");
                html.Append("<span>&nbsp;(J)&nbsp;" + details.Readings.JapaneseON + "</span>");
                html.Append("<span>&nbsp;(J)&nbsp;" + details.Readings.JapaneseKun + "</span>");
                html.Append("<span>&nbsp;(K)&nbsp;" + details.Readings.KoreanYale + "</span>");
                html.Append("<span>&nbsp;(K)&nbsp;" + details.Readings.KoreanHangul + "</span>");
                html.Append("<span>&nbsp;(K)&nbsp;" + details.Readings.KoreanHangulHun + "</span>");

                if (details.Numeric != null && details.Numeric.Numeric > -1L)
                {
                    html.Append("<span>&nbsp;(N)&nbsp;" + details.Numeric.Numeric.ToString() + "</span>");
                }

                html.Append("</div>");
                html.Append("<br />");
            }
        }

        html.Append("</body>");
        html.Append("</html>");

        var date = DateTime.Now;

        var folder = Path.Combine(_env.ContentRootPath, "wwwroot\\docs\\");

        var filename = $"{date.Year.ToString()}" +
            $"{date.Month.ToString().PadLeft(2, '0')}" +
            $"{date.Day.ToString().PadLeft(2, '0')}_" +
            $"{date.Hour.ToString().PadLeft(2, '0')}" +
            $"{date.Minute.ToString().PadLeft(2, '0')}" +
            $"{date.Second.ToString().PadLeft(2, '0')}.html";

        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        var path = Path.Combine(folder, filename);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            using var streamWriter = new StreamWriter(stream);

            await streamWriter.WriteAsync(html.ToString());
        }

        return filename;
    }

    #endregion

    #region "Private Methods"

    private static string GetCharacterChar(CharacterType characterType)
    {
        string characterTypeChar = string.Empty;

        switch (characterType)
        {
            case CharacterType.Cantonese:
                {
                    characterTypeChar = CharacterTypeChar.CANTONESE;
                }
                break;

            case CharacterType.Japanese:
                {
                    characterTypeChar = CharacterTypeChar.JAPANESE;
                }
                break;

            case CharacterType.Korean:
                {
                    characterTypeChar = CharacterTypeChar.KOREAN;
                }
                break;

            case CharacterType.Mandarin:
                {
                    characterTypeChar = CharacterTypeChar.MANDARIN;
                }
                break;

            case CharacterType.Mandarin100:
                {
                    characterTypeChar = CharacterTypeChar.MANDARIN100;
                }
                break;
        }

        return characterTypeChar;
    }

    #endregion
}
