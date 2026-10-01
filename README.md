# HanBase

Chinese Ideograms Web Interface to the Unihan Database of 70,000+ Chinese Characters

The Unihan Database is the official repository for the Unicode Consortium containing collective knowledge, mapping data, and structural analyses for Han ideographs (CJK unified characters) in the Unicode Standard.
It serves as the primary resource for converting between Unicode and legacy character sets, and provides detailed information on pronunciations, definitions, and variant characters for over 71,000 ideographs.

Key aspects of the Unihan Database include:

Official Documentation: The database is formally described in UAX #38 (Unicode Technical Annex #38), which details the organization, content, and status of its various fields.

Data Structure: Fields are named with ASCII letters and digits, starting with a lowercase "k" (e.g., kMandarin, kDefinition), and are distributed in a zipped archive called Unihan.zip within the Unicode Character Database (UCD).

Access Methods: Users can access data via the Unihan Database Lookup tool on the Unicode website, which allows searching by hexadecimal code point, character, or specific properties like Mandarin readings or definitions.

Indices: The database includes a Unihan Grid Index (grouping characters in blocks of 256) and a Unihan Radical-Stroke Index for browsing characters.

Community and Tools: The unicode-org/unihan-database GitHub repository is used for expert review of draft changes.
Various third-party tools, such as unihan-etl (for exporting to CSV/JSON), libUnihan (a normalized SQLite library), and Unicode::Unihan (Perl module), facilitate programmatic access and integration.

HanBase is a Web app interface onto the Unihan database written as a standalone Web service (Kestrel) Windows Service, written in C# .NET Core Razor Pages using Visual Studio 2022.

Features

One stop shop language learning resource for Mandarin, Cantonese, Japanese and Korean (CJK languages)

Search for Chinese characters (Ideograms) by criteria — English, PinYin, JyutPing, Yale, Unicode Code Point, et al.

Search for Chinese characters by Radical and Stroke Count

Review a Chinese character — English meaning, Mandarin, Cantonese, Japanese and Korean readings

Review a Chinese character — ideogram, ancient ideogram if one exists, Korean Hangul, variants, radicals

View charts — Korean Hangul, Japanese Hiragana and Katakana, Chinese Bopomofo (Zhuyin Fuhao), Suzhou numerals

Grammar section for learning the four main oriental languages side-by-side — learn to read, write and speak the CJK languages

Training section for learning the Chinese characters based on the official government educational lists in China, Korea and Japan

Export the training lists in a convenient HTML format for rapid and easy access
