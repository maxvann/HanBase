using System.Globalization;

namespace HanBase.Extensions;

public static class Extensions
{
    /// <summary>
    /// Convert "U+XXXX" codepoint to the escaped unicode value "\uXXXX".
    /// </summary>
    /// <param name="unicode">Codepoint to convert.</param>
    /// <returns>Converted codepoint to unicode.</returns>
    public static string ToUnicode(this string unicode)
    {
        if (unicode.StartsWith("U+"))
        {
            unicode = char.ConvertFromUtf32(int.Parse(unicode.Replace("U+", ""), NumberStyles.HexNumber));
        }

        return unicode;
    }
}
