namespace Tournament.EnumerationSlider
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// This class is used by the AsWordsConverter class.
    /// Extension methods for the String class.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Puts a blank between embedded capitals, and lowers the case.
        /// </summary>
        /// <example>
        /// "AsWords" becomes "As words".
        /// </example>
        public static string AsWords(this string source)
        {
            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

            return source.Substring(0, 1) + r.Replace(source.Substring(1), " ").ToLower();
        }
    }
}