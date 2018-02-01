// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System.Linq;
    using System.Text;
    using Catel;

    public static class StringExtensions
    {
        public static string ToCamelCase(this string input)
        {
            Argument.IsNotNullOrWhitespace(() => input);

            // Remove all not alphanumeric characters. Leave white spaces.
            var cleanChars = input.Where(c => (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))).ToArray();

            var words = new string(cleanChars).Split(' ');
            var sb = new StringBuilder();

            foreach (var word in words)
            {
                if (string.IsNullOrWhiteSpace(word) || string.IsNullOrWhiteSpace(word))
                {
                    continue;
                }

                var firstLetter = word.Substring(0, 1).ToUpper();
                var rest = word.Substring(1, word.Length - 1);
                sb.Append(firstLetter + rest);
            }

            return sb.ToString();
        }
    }
}