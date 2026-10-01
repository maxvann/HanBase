using HanBase.Constants;
using HanBase.Entities;

namespace HanBase.Interfaces;

public interface ICharacterDetails
{
    Task<CharacterDefinition?> GetCharacter(string unicode);

    Task<List<CharacterDefinition>?> GetCharacterRadicals(int radicalNumber);

    Task<List<CharacterDefinition>?> GetCharacterVariants(string unicode);

    Task<List<CharacterDefinition>?> GetCharacters(int radicalNumber, int strokeCount);

    Task<List<CharacterDefinition>?> GetCharacters(List<CharacterVariant> variants);

    Task<string> GetDefinition(string unicode);

    Task GetDefinitions(List<CharacterDefinition> characters);

    Task<string> GetKoreanHunReading(string unicode);

    Task<CharacterNumeric?> GetNumericValue(string unicode);

    Task<CharacterReadings> GetReadings(string unicode);

    Task<string[]> GetTrainingCharacters(string languageType, int page, int pageSize);

    Task<int> GetTrainingCharactersCount(string languageType);

    Task<List<CharacterVariant>?> GetVariants(string unicode);

    Task<List<CharacterDefinition>> SearchCharacters(SearchCriteria criteria, string search);

    CharacterReadings? TransformReadings(CharacterReadings? readings);
}
