using System.Globalization;
using System.Text.RegularExpressions;

namespace HanBase.Extensions;

public static class Extensions
{
    /// <summary>
    /// Convert "U+XXXX" codepoint to the escaped unicode value "\uXXXX".
    /// </summary>
    /// <param name="unicode">Codepoint or list of codepoints to convert.</param>
    /// <returns>Converted codepoint to unicode.</returns>
    public static string ToUnicode(this string unicode)
    {
        List<string> transformWords = [];
        string[] unicodeWords = [.. Regex.Matches(unicode, @"U\+[0-9A-Fa-f]{1,6}").Select(x => x.Value)];

        if (unicodeWords == null || unicodeWords.Length == 0)
        {
            return string.Empty;
        }

        foreach (string word in unicodeWords)
        {
            string transformWord = char.ConvertFromUtf32(int.Parse(word.Replace("U+", ""), NumberStyles.HexNumber));

            transformWords.Add(transformWord);
        }

        return string.Join("", [.. transformWords]);
    }
}
