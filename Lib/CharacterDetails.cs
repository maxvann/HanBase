using HanBase.Constants;
using HanBase.Data;
using HanBase.Entities;
using HanBase.Extensions;
using HanBase.Interfaces;
using HanBase.Models;
using HanBase.Statics;
using Microsoft.EntityFrameworkCore;

namespace HanBase.Lib;

public class CharacterDetails : ICharacterDetails
{
    private readonly HanBaseContext _context;

    private const int MAXIMUM_COUNT = 500;

    public CharacterDetails(HanBaseContext context)
    {
        _context = context;
    }

    #region "Public Methods"

    /// <summary>
    /// Search for a character based on the Unicode code point entry.
    /// </summary>
    /// <param name="unicode">Unicode "U+XXXX" codepoint.</param>
    /// <returns>Details of the character.</returns>
    public async Task<CharacterDefinition?> GetCharacter(string unicode)
    {
        CharacterDefinition? character = null;
        RadicalStrokeCount? radical = null;

        radical = await _context.RadicalStrokeCounts
                    .Where(x => x.Unicode.Equals(unicode))
                    .FirstOrDefaultAsync();

        if (radical == null)
        {
            return null;
        }

        character = new CharacterDefinition
        {
            Unicode = radical.Unicode,
            Ideogram = radical.Unicode.ToUnicode(),
            VariantType = string.Empty,
            TranslatedReading = string.Empty,
            RadicalNumber = radical.RadicalNumber,
            StrokeCount = radical.StrokeCount,
            IsInMap = false
        };

        character.IsInMap = await IsUnicodeInMap(character.Unicode);

        return character;
    }

    public async Task<List<CharacterDefinition>?> GetCharacterRadicals(int radicalNumber)
    {
        List<CharacterDefinition>? characters = await GetCharacters(radicalNumber, 0);

        if (characters != null && characters.Count > 0)
        {
            await GetDefinitions(characters);
        }

        return characters;
    }

    public async Task<List<CharacterDefinition>?> GetCharacterVariants(string unicode)
    {
        List<CharacterVariant>? variants = await GetVariants(unicode);

        if (variants == null || variants.Count == 0)
        {
            return null;
        }

        List<CharacterDefinition>? characters = await GetCharacters(variants);

        if (characters == null || characters.Count == 0)
        {
            return null;
        }

        await GetDefinitions(characters);

        return characters;
    }

    /// <summary>
    /// Search for a list of characters by radical ID and stroke count.
    /// </summary>
    /// <param name="radicalNumber">Radical ID.</param>
    /// <param name="strokeCount">Character stroke count.</param>
    /// <returns>All the characters for the given radical and stroke count.</returns>
    public async Task<List<CharacterDefinition>?> GetCharacters(int radicalNumber, int strokeCount)
    {
        List<CharacterDefinition>? characters = null;
        List<RadicalStrokeCount>? radicals = null;

        radicals = await _context.RadicalStrokeCounts
            .Where(x => x.RadicalNumber == radicalNumber && x.StrokeCount == strokeCount)
            .OrderBy(x => x.RadicalNumber)
            .ToListAsync();

        if (radicals != null && radicals.Count > 0)
        {
            characters = [];

            foreach (var radical in radicals)
            {
                characters.Add(new CharacterDefinition
                {
                    Unicode = radical.Unicode,
                    Ideogram = radical.Unicode.ToUnicode(),
                    VariantType = string.Empty,
                    TranslatedReading = string.Empty,
                    RadicalNumber = radical.RadicalNumber,
                    StrokeCount = radical.StrokeCount
                });
            }

            await GetDefinitions(characters);
        }

        return characters;
    }

    /// <summary>
    /// Get more information about a variant character.
    /// </summary>
    /// <param name="variants">List of variants to check.</param>
    /// <returns>List of characters containing more information.</returns>
    public async Task<List<CharacterDefinition>?> GetCharacters(List<CharacterVariant> variants)
    {
        List<CharacterDefinition>? characters = null;
        RadicalStrokeCount? radical = null;
        string relatedUnicode = string.Empty;

        if (variants == null || variants.Count == 0)
        {
            return null;
        }

        characters = [];

        foreach (var variant in variants)
        {
            relatedUnicode = variant.RelatedUnicode;

            if (relatedUnicode.IndexOf('<') > -1)
            {
                relatedUnicode = relatedUnicode[..relatedUnicode.IndexOf('<')];
            }

            radical = await _context.RadicalStrokeCounts
                .Where(x => x.Unicode.Equals(relatedUnicode))
                .FirstOrDefaultAsync();

            if (radical != null)
            {
                characters.Add(new CharacterDefinition
                {
                    Unicode = radical.Unicode,
                    Ideogram = radical.Unicode.ToUnicode(),
                    VariantType = variant.VariantType,
                    TranslatedReading = string.Empty,
                    RadicalNumber = radical.RadicalNumber,
                    StrokeCount = radical.StrokeCount
                });
            }
        }

        return characters;
    }

    /// <summary>
    /// Get the English definition of the specified character.
    /// </summary>
    /// <param name="unicode">Unicode code point of the character.</param>
    /// <returns>English definition.</returns>
    public async Task<string> GetDefinition(string unicode)
    {
        string? definition = string.Empty;

        var result = await _context.Readings
            .Where(x => x.RelatedUnicode.Equals(unicode) && x.LanguageType.Equals(LanguageDefinition.DEFINITION))
            .Take(1)
            .FirstOrDefaultAsync();

        definition = result?.TranslatedReading;

        return definition ?? string.Empty;
    }

    /// <summary>
    /// Get the English definition of the list of characters.
    /// </summary>
    /// <param name="characters">List of characters to update.</param>
    public async Task GetDefinitions(List<CharacterDefinition> characters)
    {
        if (characters != null && characters.Count > 0)
        {
            foreach (var character in characters)
            {
                character.TranslatedReading = await GetDefinition(character.Unicode);
            }
        }
    }

    public async Task<string> GetKoreanHunReading(string unicode)
    {
        if (string.IsNullOrEmpty(unicode))
        {
            return string.Empty;
        }

        var result = await _context.KoreanHunReadings
            .Where(x => x.RelatedUnicode.Equals(unicode))
            .FirstOrDefaultAsync();

        if (result == null)
        {
            return string.Empty;
        }

        return result.TranslatedReading;
    }

    public async Task<CharacterNumeric?> GetNumericValue(string unicode)
    {
        CharacterNumeric? characterNumeric = null;

        var numericValue = await _context.NumericValues
            .Where(x => x.RelatedUnicode.Equals(unicode))
            .FirstOrDefaultAsync();

        if (numericValue != null)
        {
            characterNumeric = new CharacterNumeric
            {
                RelatedUnicode = numericValue.RelatedUnicode,
                NumericType = numericValue.NumericType,
                Numeric = numericValue.Numeric
            };
        }

        return characterNumeric;
    }

    /// <summary>
    /// Get the various CJK readings of the specified character. 
    /// </summary>
    /// <param name="unicode">Unicode code point of the character.</param>
    /// <returns>CJK readings.</returns>
    public async Task<CharacterReadings> GetReadings(string unicode)
    {
        CharacterReadings readings = new();

        var results = await _context.Readings
            .Where(x => x.RelatedUnicode.Equals(unicode))
            .ToListAsync();

        if (results != null && results.Count > 0)
        {
            readings.EnglishDefinition = results.Where(x => x.LanguageType.Equals(LanguageDefinition.DEFINITION)).FirstOrDefault()?.TranslatedReading ?? string.Empty;
            readings.MandarinPinYin = results.Where(x => x.LanguageType.Equals(LanguageDefinition.MANDARIN)).FirstOrDefault()?.TranslatedReading ?? string.Empty;
            readings.CantoneseJyutPing = results.Where(x => x.LanguageType.Equals(LanguageDefinition.CANTONESE)).FirstOrDefault()?.TranslatedReading ?? string.Empty;
            readings.JapaneseON = results.Where(x => x.LanguageType.Equals(LanguageDefinition.JAPANESE_ON)).FirstOrDefault()?.TranslatedReading ?? string.Empty;
            readings.JapaneseKun = results.Where(x => x.LanguageType.Equals(LanguageDefinition.JAPANESE_KUN)).FirstOrDefault()?.TranslatedReading.ToLower() ?? string.Empty;
            readings.KoreanYale = results.Where(x => x.LanguageType.Equals(LanguageDefinition.KOREAN)).FirstOrDefault()?.TranslatedReading ?? string.Empty;
            readings.KoreanHangul = results.Where(x => x.LanguageType.Equals(LanguageDefinition.HANGUL)).FirstOrDefault()?.TranslatedReading ?? string.Empty;
        }

        return readings;
    }

    public async Task<string[]> GetTrainingCharacters(string languageType, int page, int pageSize)
    {
        if (languageType == string.Empty || page == 0 || pageSize == 0)
        {
            return [];
        }

        var characters = await _context.TrainingCharacters
                .Where(x => x.LanguageType.Equals(languageType))
                .Select(x => x.CharacterUnicode)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToArrayAsync();

        return characters;
    }

    public async Task<int> GetTrainingCharactersCount(string languageType)
    {
        if (languageType == string.Empty)
        {
            return 0;
        }

        var totalCharacters = await _context.TrainingCharacters
            .CountAsync(x => x.LanguageType.Equals(languageType));

        return totalCharacters;
    }

    /// <summary>
    /// Get the related variant characters for a given unicode character.
    /// </summary>
    /// <param name="unicode">Unicode code point of the character.</param>
    /// <returns>List of variant characters.</returns>
    public async Task<List<CharacterVariant>?> GetVariants(string unicode)
    {
        List<CharacterVariant>? variants = null;

        var variantResults = await _context.Variants
            .Where(x => x.Unicode.Equals(unicode))
            .ToListAsync();

        if (variantResults != null && variantResults.Count > 0)
        {
            variants = [];

            foreach (var variant in variantResults)
            {
                variants.Add(new CharacterVariant
                {
                    Unicode = variant.Unicode,
                    VariantType = variant.VariantType,
                    RelatedUnicode = variant.RelatedUnicode
                });
            }
        }

        return variants;
    }

    /// <summary>
    /// Search for characters based on various search criteria and search string.
    /// </summary>
    /// <param name="criteria">Search criteria.</param>
    /// <param name="search">Search string.</param>
    /// <returns>List of found characters.</returns>
    public async Task<List<CharacterDefinition>> SearchCharacters(SearchCriteria criteria, string search)
    {
        List<Reading> readings = [];
        List<CharacterDefinition> characters = [];

        IQueryable<Reading> query = criteria switch
        {
            SearchCriteria.EnglishTranslation =>
                _context.Readings.Where(x => x.TranslatedReading.Contains(search)),

            SearchCriteria.MandarinPinYin =>
                _context.Readings.Where(x => x.LanguageType == LanguageDefinition.MANDARIN && x.TranslatedReading.Contains(search)),

            SearchCriteria.CantoneseJyutPing =>
                _context.Readings.Where(x => x.LanguageType == LanguageDefinition.CANTONESE && x.TranslatedReading.Contains(search)),

            SearchCriteria.JapaneseON =>
                _context.Readings.Where(x => x.LanguageType == LanguageDefinition.JAPANESE_ON && x.TranslatedReading.Contains(search)),

            SearchCriteria.JapaneseKun =>
                _context.Readings.Where(x => x.LanguageType == LanguageDefinition.JAPANESE_KUN && x.TranslatedReading.Contains(search)),

            SearchCriteria.KoreanYale =>
                _context.Readings.Where(x => x.LanguageType == LanguageDefinition.KOREAN && x.TranslatedReading.Contains(search)),

            SearchCriteria.UnicodeCodePoint =>
                _context.Readings.Where(x => x.RelatedUnicode == search),

            _ => Enumerable.Empty<Reading>().AsQueryable()
        };

        readings = await query
            .Take(MAXIMUM_COUNT)
            .ToListAsync();

        if (readings != null && readings.Count > 0)
        {
            foreach (var reading in readings)
            {
                characters.Add(new CharacterDefinition
                {
                    Unicode = reading.RelatedUnicode,
                    Ideogram = reading.RelatedUnicode.ToUnicode(),
                    VariantType = string.Empty,
                    TranslatedReading = reading.TranslatedReading,
                    RadicalNumber = 0,
                    StrokeCount = 0
                });
            }

            await GetDefinitions(characters);
        }

        return characters;
    }

    /// <summary>
    /// Convert Cantonese JyutPing and Hangul Yale to another reading system.
    /// </summary>
    /// <param name="readings">The readings of the character.</param>
    /// <returns>The modified Cantonese and Korean pronunciations.</returns>
    public CharacterReadings? TransformReadings(CharacterReadings? readings)
    {
        if (readings == null)
        {
            return readings;
        }

        readings.CantoneseJyutPing = Transform.CantoneseJyutPingToYale(readings.CantoneseJyutPing);
        readings.KoreanYale = Transform.KoreanYaleToMcCuneReischauer(readings.KoreanYale);

        return readings;

    }

    #endregion

    #region "Private Methods"

    /// <summary>
    /// Search for a Unicode code point entry in the Seal Font map.
    /// </summary>
    /// <param name="unicode">Unicode "U+XXXX" codepoint.</param>
    /// <returns>Whether the unicode has been found or not.</returns>
    private async Task<bool> IsUnicodeInMap(string unicode)
    {
        var map = await _context.SealFonts
                    .Where(x => x.Unicode.Equals(unicode))
                    .FirstOrDefaultAsync();

        if (map == null || map.Unicode == string.Empty)
        {
            return false;
        }

        return true;
    }

    #endregion
}
