namespace HanBase.Constants;

public enum SearchCriteria
{
    EnglishTranslation = 0,
    MandarinPinYin = 1,
    CantoneseJyutPing = 2,
    JapaneseON = 3,
    JapaneseKun = 4,
    KoreanYale = 5,
    UnicodeCodePoint = 6
}

public enum CharacterType
{
    Cantonese = 0,
    Japanese = 1,
    Korean = 2,
    Mandarin = 3,
    Mandarin100 = 4
}

public static class CharacterTypeChar
{
    public const string CANTONESE = "C";
    public const string JAPANESE = "J";
    public const string KOREAN = "K";
    public const string MANDARIN = "M";
    public const string MANDARIN100 = "X";
}

public static class LanguageDefinition
{
    public const string DEFINITION = "kDefinition";
    public const string MANDARIN = "kMandarin";
    public const string CANTONESE = "kCantonese";
    public const string JAPANESE_ON = "kJapaneseOn";
    public const string JAPANESE_KUN = "kJapaneseKun";
    public const string KOREAN = "kKorean";
    public const string HANGUL = "kHangul";
}

public static class Pages
{
    public const int ReadingsPageSize = 20;

    public const int TrainingPageSize = 250;
}

public static class Radicals
{
    private static readonly string[] RADICALS = [
        "U+4E00", "U+4E28", "U+4E36", "U+4E3F", "U+4E59", "U+4E85", "U+4E8C", "U+4EA0", "U+4EBA", "U+513F",
        "U+5165", "U+516B", "U+5182", "U+5196", "U+51AB", "U+51E0", "U+51F5", "U+5200", "U+529B", "U+52F9",
        "U+5315", "U+531A", "U+5338", "U+5341", "U+535C", "U+5369", "U+5382", "U+53B6", "U+53C8", "U+53E3",
        "U+56D7", "U+571F", "U+58EB", "U+5902", "U+590A", "U+5915", "U+5927", "U+5973", "U+5B50", "U+5B80",
        "U+5BF8", "U+5C0F", "U+5C22", "U+5C38", "U+5C6E", "U+5C71", "U+5DDD", "U+5DE5", "U+5DF1", "U+5DFE",
        "U+5E72", "U+5E7A", "U+5E7F", "U+5EF4", "U+5EFE", "U+5F0B", "U+5F13", "U+5F51", "U+5F61", "U+5F73",
        "U+5FC3", "U+6208", "U+6236", "U+624B", "U+652F", "U+6535", "U+6587", "U+6597", "U+65A4", "U+65B9",
        "U+65E0", "U+65E5", "U+66F0", "U+6708", "U+6728", "U+6B20", "U+6B62", "U+6B79", "U+6BB3", "U+6BCB",
        "U+6BD4", "U+6BDB", "U+6C0F", "U+6C14", "U+6C34", "U+706B", "U+722A", "U+7236", "U+723B", "U+723F",
        "U+7247", "U+7259", "U+725B", "U+72AC", "U+7384", "U+7389", "U+74DC", "U+74E6", "U+7518", "U+751F",
        "U+7528", "U+7530", "U+758B", "U+7592", "U+7676", "U+767D", "U+76AE", "U+76BF", "U+76EE", "U+77DB",
        "U+77E2", "U+77F3", "U+793A", "U+79B8", "U+79BE", "U+7A74", "U+7ACB", "U+7AF9", "U+7C73", "U+7CF8",
        "U+7F36", "U+7F51", "U+7F8A", "U+7FBD", "U+8001", "U+800C", "U+8012", "U+8033", "U+807F", "U+8089",
        "U+81E3", "U+81EA", "U+81F3", "U+81FC", "U+820C", "U+821B", "U+821F", "U+826E", "U+8272", "U+8278",
        "U+864D", "U+866B", "U+8840", "U+884C", "U+8863", "U+897E", "U+898B", "U+89D2", "U+8A00", "U+8C37",
        "U+8C46", "U+8C55", "U+8C78", "U+8C9D", "U+8D64", "U+8D70", "U+8DB3", "U+8EAB", "U+8ECA", "U+8F9B",
        "U+8FB0", "U+8FB5", "U+9091", "U+9149", "U+91C6", "U+91CC", "U+91D1", "U+9577", "U+9580", "U+961C",
        "U+96B6", "U+96B9", "U+96E8", "U+9751", "U+975E", "U+9762", "U+9769", "U+97CB", "U+97ED", "U+97F3",
        "U+9801", "U+98A8", "U+98DB", "U+98DF", "U+9996", "U+9999", "U+99AC", "U+9AA8", "U+9AD8", "U+9ADF",
        "U+9B25", "U+9B2F", "U+9B32", "U+9B3C", "U+9B5A", "U+9CE5", "U+9E75", "U+9E7F", "U+9EA5", "U+9EBB",
        "U+9EC3", "U+9ECD", "U+9ED1", "U+9EF9", "U+9EFD", "U+9F0E", "U+9F13", "U+9F20", "U+9F3B", "U+9F4A",
        "U+9F52", "U+9F8D", "U+F908", "U+9FA0"
        ];

    public static string[] GetRadicals()
    {
        return RADICALS;
    }
}

public static class Charts
{
    private static readonly string[] HANGUL = [
        "U+3131", "U+3132", "U+3134", "U+3137", "U+3138", "U+3139", "U+3141", "",
        "U+3142", "U+3143", "U+3145", "U+3146", "U+3147", "U+3148", "U+3149", "",
        "U+314A", "U+314B", "U+314C", "U+314D", "U+314E", "", "", "",
        "U+314F", "U+3150", "U+3151", "U+3152", "U+3153", "U+3154", "U+3155", "U+3156",
        "U+3157", "U+3158", "U+3159", "U+315A", "U+315B", "U+315C", "U+315D", "",
        "U+315E", "U+315F", "U+3160", "U+3161", "U+3162", "U+3163", "", ""
        ];

    private static readonly string[] HANGUL_SOUND = [
        "k,g,ng", "kk", "n", "t,d,n", "tt", "l,r,n", "m", "",
        "p,b,m", "pp", "s,sh,t", "ss,ssh,t", "0,ng", "ch,j,t", "cch", "",
        "ch',t", "k'", "t'", "p'", "h,t", "", "", "",
        "a", "ae", "ya", "yae", "ŏ", "e", "yŏ", "ye",
        "o", "wa", "wae", "oe", "yo", "u", "wŏ", "",
        "we", "wi", "yu", "ŭ", "ŭy", "i", "", ""
        ];

    private static readonly string[] HIRAGANA = [
        "U+3042", "U+304B", "U+3055", "U+305F", "U+306A", "U+306F", "U+307E", "U+3084", "U+3089", "U+308F",
        "U+3044", "U+304D", "U+3057", "U+3061", "U+306B", "U+3072", "U+307F", "U+3044", "U+308A", "U+3090",
        "U+3046", "U+304F", "U+3059", "U+3064", "U+306C", "U+3075", "U+3080", "U+3086", "U+308B", "U+3046",
        "U+3048", "U+3051", "U+305B", "U+3066", "U+306D", "U+3078", "U+3081", "U+3048", "U+308C", "U+3091",
        "U+304A", "U+3053", "U+305D", "U+3068", "U+306E", "U+307B", "U+3082", "U+3088", "U+308D", "U+3092",
        "", "", "", "", "U+3093", "","","","", ""
        ];

    private static readonly string[] HIRAGANA_SOUND = [
        "a", "ka", "sa", "ta", "na", "ha", "ma", "ya", "ra", "wa",
        "i", "ki", "shi", "chi", "ni", "hi", "mi", "yi", "ri", "wi",
        "u", "ku", "su", "tsu", "nu", "fu", "mu", "yu", "ru", "wu",
        "e", "ke", "se", "te", "ne", "he", "me", "ye", "re", "we",
        "o", "ko", "so", "to", "no", "ho", "mo", "yo", "ro", "wo,o",
        "", "", "", "", "n", "","","","", ""
        ];

    private static readonly string[] HIRAGANA_EXT = [
        "U+304C", "U+3056", "U+3060", "U+3070", "U+3071", "U+3041", "", "U+3083", "U+308E",
        "U+304E", "U+3058", "U+3062", "U+3073", "U+3074", "U+3043", "", "", "",
        "U+3050", "U+305A", "U+3065", "U+3076", "U+3077", "U+3045", "U+3063", "U+3085", "",
        "U+3052", "U+305C", "U+3067", "U+3079", "U+307A", "U+3047", "", "", "",
        "U+3054", "U+305E", "U+3069", "U+307C", "U+307D", "U+3049", "", "U+3087", ""
        ];

    private static readonly string[] HIRAGANA_EXT_SOUND = [
        "ga", "za", "da", "ba", "pa", "a", "", "ya", "wa",
        "gi", "ji", "ji", "bi", "pi", "i", "", "", "",
        "gu", "zu", "zu", "bu", "pu", "u", "tsu", "yu", "",
        "ge", "ze", "de", "be", "pe", "e", "", "", "",
        "go", "zo", "do", "bo", "po", "o", "", "yo", ""
        ];

    private static readonly string[] KATAKANA = [
        "U+30A2", "U+30AB", "U+30B5", "U+30BF", "U+30CA", "U+30CF", "U+30DE", "U+30E4", "U+30E9", "U+30EF",
        "U+30A4", "U+30AD", "U+30B7", "U+30C1", "U+30CB", "U+30D2", "U+30DF", "U+30A4", "U+30EA", "U+30F0",
        "U+30A6", "U+30AF", "U+30B9", "U+30C4", "U+30CC", "U+30D5", "U+30E0", "U+30E6", "U+30EB", "U+30A6",
        "U+30A8", "U+30B1", "U+30BB", "U+30C6", "U+30CD", "U+30D8", "U+30E1", "U+30A8", "U+30EC", "U+30F1",
        "U+30AA", "U+30B3", "U+30BD", "U+30C8", "U+30CE", "U+30DB", "U+30E2", "U+30E8", "U+30ED", "U+30F2",
        "", "", "", "", "U+30F3", "","","","", ""
        ];

    private static readonly string[] KATAKANA_SOUND = [
        "a", "ka", "sa", "ta", "na", "ha", "ma", "ya", "ra", "wa",
        "i", "ki", "shi", "chi", "ni", "hi", "mi", "yi", "ri", "wi",
        "u", "ku", "su", "tsu", "nu", "fu", "mu", "yu", "ru", "wu",
        "e", "ke", "se", "te", "ne", "he", "me", "ye", "re", "we",
        "o", "ko", "so", "to", "no", "ho", "mo", "yo", "ro", "wo",
        "", "", "", "", "n", "","","","", ""
        ];

    private static readonly string[] KATAKANA_EXT = [
        "U+30AC", "U+30B6", "U+30C0", "U+30D0", "U+30D1", "U+30A1", "", "U+30E3", "U+30EE",
        "U+30AE", "U+30B8", "U+30C2", "U+30D3", "U+30D4", "U+30A3", "", "", "",
        "U+30B0", "U+30BA", "U+30C5", "U+30D6", "U+30D7", "U+30A5", "U+30C3", "U+30E5", "",
        "U+30B2", "U+30BC", "U+30C7", "U+30D9", "U+30DA", "U+30A7", "", "", "",
        "U+30B4", "U+30BE", "U+30C9", "U+30DC", "U+30DD", "U+30A9", "","U+30E7", "U+30FC"
        ];

    private static readonly string[] KATAKANA_EXT_SOUND = [
        "ga", "za", "da", "ba", "pa", "a", "", "ya", "wa",
        "gi", "ji", "ji", "bi", "pi", "i", "", "", "",
        "gu", "zu", "zu", "bu", "pu", "u", "tsu", "yu", "",
        "ge", "ze", "de", "be", "pe", "e", "", "", "",
        "go", "zo", "do", "bo", "po", "o", "", "yo", "length"
        ];

    private static readonly string[] BOPOMOFO = [
        "U+3105", "U+3106", "U+3107", "U+3108", "U+3109", "U+310A", "U+310B",
        "U+310C", "U+310D", "U+310E", "U+310F", "U+3110", "U+3111", "U+3112",
        "U+3113", "U+3114", "U+3115", "U+3116", "U+3117", "U+3118", "U+3119",
        "U+311A", "U+311B", "U+311C", "U+311D", "U+311E", "U+311F", "U+3120",
        "U+3121", "U+3122", "U+3123", "U+3124", "U+3125", "U+3126", "",
        "U+3128", "U+3129", "U+312A", "U+312B", "U+312C", "U+312D", "U+3127"
        ];

    private static readonly string[] BOPOMOFO_SOUND = [
        "b", "p", "m", "f", "d", "t", "n",
        "l", "g", "k", "h", "j", "q", "x",
        "zh", "chi", "shi", "r", "z", "c", "s",
        "a", "o", "e", "eh", "ai", "ei", "ao",
        "ou", "an", "en", "ang", "eng", "er", "",
        "wu", "yu", "v", "ng", "ny", "ih", "yi"
        ];

    private static readonly string[] SUZHOU = [
        "U+3007", "U+3021", "U+3022", "U+3023", "U+3024",
        "U+3025", "U+3026", "U+3027", "U+3028", "U+3029",
        "U+5341", "U+5EFF", "U+5345", "U+534C", ""
        ];

    private static readonly string[] SUZHOU_SOUND = [
        "0", "1", "2", "3", "4",
        "5", "6", "7", "8", "9",
        "10", "20", "30", "40", ""
        ];

    public static string[] GetHangul()
    {
        return HANGUL;
    }

    public static string[] GetHangulSound()
    {
        return HANGUL_SOUND;
    }

    public static string[] GetHiragana()
    {
        return HIRAGANA;
    }

    public static string[] GetHiraganaSound()
    {
        return HIRAGANA_SOUND;
    }

    public static string[] GetHiraganaExt()
    {
        return HIRAGANA_EXT;
    }

    public static string[] GetHiraganaExtSound()
    {
        return HIRAGANA_EXT_SOUND;
    }

    public static string[] GetKatakana()
    {
        return KATAKANA;
    }

    public static string[] GetKatakanaSound()
    {
        return KATAKANA_SOUND;
    }

    public static string[] GetKatakanaExt()
    {
        return KATAKANA_EXT;
    }

    public static string[] GetKatakanaExtSound()
    {
        return KATAKANA_EXT_SOUND;
    }

    public static string[] GetBopomofo()
    {
        return BOPOMOFO;
    }

    public static string[] GetBopomofoSound()
    {
        return BOPOMOFO_SOUND;
    }

    public static string[] GetSuzhou()
    {
        return SUZHOU;
    }

    public static string[] GetSuzhouSound()
    {
        return SUZHOU_SOUND;
    }
}
