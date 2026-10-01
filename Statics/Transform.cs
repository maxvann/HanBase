namespace HanBase.Statics;

public static class Transform
{
    private static readonly string NEUTRAL = "aeiou";
    private static readonly string MIDDLE = "āēīōū";
    private static readonly string FALLING = "àèìòù";
    private static readonly string RISING = "áéíóú";
    private static readonly string LOW_TONE = "ℏ";
    private static readonly string TONE_NUMBERS = "123456";

    #region "Public Methods"

    public static string CantoneseJyutPingToYale(string cantoneseJyutPingText)
    {
        if (cantoneseJyutPingText == null || cantoneseJyutPingText.Length == 0)
        {
            return string.Empty;
        }

        List<string> transformWords = [];
        string[] cantoneseJyutPingWords = cantoneseJyutPingText.Split(' ');

        if (cantoneseJyutPingWords == null || cantoneseJyutPingWords.Length == 0)
        {
            return string.Empty;
        }

        foreach (string word in cantoneseJyutPingWords)
        {
            string transformWord = word.ToLower();

            // General conversion

            if (transformWord.StartsWith("jy"))
            {
                transformWord = transformWord.Remove(0, 2);
                transformWord = transformWord.Insert(0, "y");
            }

            if (transformWord.StartsWith("j"))
            {
                transformWord = transformWord.Remove(0, 1);
                transformWord = transformWord.Insert(0, "y");
            }

            if (transformWord.StartsWith("z"))
            {
                transformWord = transformWord.Remove(0, 1);
                transformWord = transformWord.Insert(0, "j");
            }

            if (transformWord.StartsWith("c"))
            {
                transformWord = transformWord.Remove(0, 1);
                transformWord = transformWord.Insert(0, "ch");
            }

            if (transformWord.Contains("eo"))
            {
                transformWord = transformWord.Replace("eo", "eu");
            }

            if (transformWord.Contains("oe"))
            {
                transformWord = transformWord.Replace("oe", "eu");
            }

            // Vowel tone conversion

            if (transformWord[transformWord.Length - 1] == '1')
            {
                transformWord = RemoveToneNumber(transformWord);
                transformWord = ChangeVowelTone(transformWord, FALLING);
            }
            else if (transformWord[transformWord.Length - 1] == '2')
            {
                transformWord = RemoveToneNumber(transformWord);
                transformWord = ChangeVowelTone(transformWord, RISING);
            }
            else if (transformWord[transformWord.Length - 1] == '3')
            {
                transformWord = RemoveToneNumber(transformWord);
                transformWord = ChangeVowelTone(transformWord, MIDDLE);
            }
            else if (transformWord[transformWord.Length - 1] == '4')
            {
                transformWord = RemoveToneNumber(transformWord);
                transformWord = AddLowTone(transformWord);
                transformWord = ChangeVowelTone(transformWord, FALLING);
            }
            else if (transformWord[transformWord.Length - 1] == '5')
            {
                transformWord = RemoveToneNumber(transformWord);
                transformWord = AddLowTone(transformWord);
                transformWord = ChangeVowelTone(transformWord, RISING);
            }
            else if (transformWord[transformWord.Length - 1] == '6')
            {
                transformWord = RemoveToneNumber(transformWord);
                transformWord = AddLowTone(transformWord);
            }

            transformWords.Add(transformWord);
        }

        return string.Join(" ", [.. transformWords]);
    }

    public static string KoreanYaleToMcCuneReischauer(string koreanYaleText)
    {
        if (koreanYaleText == null || koreanYaleText.Length == 0)
        {
            return string.Empty;
        }

        List<string> transformWords = [];
        string[] koreanYaleWords = koreanYaleText.Split(' ');

        if (koreanYaleWords == null || koreanYaleWords.Length == 0)
        {
            return string.Empty;
        }

        foreach (string word in koreanYaleWords)
        {
            string transformWord = word.ToLower();

            // Aspirated consonants.
            if (transformWord.Contains("ch"))
            {
                transformWord = transformWord.Replace("ch", "cx");
            }

            if (transformWord.Contains("c"))
            {
                transformWord = transformWord.Replace("c", "ch");
            }

            if (transformWord.Contains("chx"))
            {
                transformWord = transformWord.Replace("chx", "ch'");
            }

            if (transformWord.Contains("kh"))
            {
                transformWord = transformWord.Replace("kh", "k'");
            }

            if (transformWord.Contains("th"))
            {
                transformWord = transformWord.Replace("th", "t'");
            }

            if (transformWord.Contains("ph"))
            {
                transformWord = transformWord.Replace("ph", "p'");
            }

            if (transformWord.Contains("si"))
            {
                transformWord = transformWord.Replace("si", "shi");
            }

            // Vowels.
            if (transformWord.Contains("wo"))
            {
                transformWord = transformWord.Replace("wo", "wx");
            }

            if (transformWord.Contains("wu"))
            {
                transformWord = transformWord.Replace("wu", "wy");
            }

            if (transformWord.Contains("ay"))
            {
                transformWord = transformWord.Replace("ay", "ae");
            }

            if (transformWord.Contains("e"))
            {
                transformWord = transformWord.Replace("e", "ŏ");
            }

            if (transformWord.Contains("ŏy"))
            {
                transformWord = transformWord.Replace("ŏy", "e");
            }

            if (transformWord.Contains("oy"))
            {
                transformWord = transformWord.Replace("oy", "oe");
            }

            if (transformWord.Contains("uy"))
            {
                transformWord = transformWord.Replace("uy", "ŭy");
            }

            if (transformWord.Contains("u"))
            {
                transformWord = transformWord.Replace("u", "ŭ");
            }

            // Fixup.
            if (transformWord.Contains("aŏ"))
            {
                transformWord = transformWord.Replace("aŏ", "ae");
            }

            if (transformWord.Contains("yŭ"))
            {
                transformWord = transformWord.Replace("yŭ", "yu");
            }

            if (transformWord.Contains("wx"))
            {
                transformWord = transformWord.Replace("wx", "o");
            }

            if (transformWord.Contains("wy"))
            {
                transformWord = transformWord.Replace("wy", "u");
            }

            transformWords.Add(transformWord);
        }

        return string.Join(" ", [.. transformWords]);
    }

    #endregion

    #region "Private Methods"

    private static string RemoveToneNumber(string target)
    {
        if (target == null || target.Length == 0)
        {
            return string.Empty;
        }

        if (TONE_NUMBERS.Contains(target[target.Length - 1]))
        {
            target = target.Remove(target.Length - 1, 1);
        }

        return target;
    }

    private static string AddLowTone(string target)
    {
        if (target == null || target.Length == 0)
        {
            return string.Empty;
        }

        if (NEUTRAL.Contains(target[target.Length - 1]))
        {
            target = target.Insert(target.Length, LOW_TONE);
        }
        else
        {
            if (target.Length > 2)
            {
                if (target.EndsWith("ng"))
                {
                    target = target.Insert(target.Length - 2, LOW_TONE);
                }
                else
                {
                    target = target.Insert(target.Length - 1, LOW_TONE);
                }
            }
            else
            {
                target = target.Insert(target.Length, LOW_TONE);
            }
        }

        return target;
    }

    private static string ChangeVowelTone(string target, string vowels)
    {
        if (target == null || target.Length == 0 || vowels == null || vowels.Length != 5)
        {
            return string.Empty;
        }

        int index = target.IndexOfAny(NEUTRAL.ToCharArray());

        if (index > 0)
        {
            int pos = NEUTRAL.IndexOf(target[index]);
            char[] chars = target.ToCharArray();
            chars[index] = vowels[pos];
            target = new string(chars);
        }

        return target;
    }

    #endregion
}
