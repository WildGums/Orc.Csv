// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringToBooleanConverterFacts.cs" company="WildGums">
//   Copyright (c) 2008 - 2018 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv.Tests.Converters
{
    using Catel.Fody;
    using Csv;
    using NUnit.Framework;

    [TestFixture]
    public class StringToBooleanConverterFacts
    {
        [TestCase("true", true)]
        [TestCase("TRUE", true)]
        [TestCase("on", true)]
        [TestCase("ON", true)]
        [TestCase("yes", true)]
        [TestCase("YES", true)]
        [TestCase("1", true)]
        [TestCase("false", false)]
        [TestCase("FALSE", false)]
        [TestCase("off", false)]
        [TestCase("OFF", false)]
        [TestCase("no", false)]
        [TestCase("NO", false)]
        [TestCase("0", false)]
        [TestCase("gibberish", false)]
        [TestCase(" TRUE   ", true)]
        public void CorrectlyConvertsValue(string input, bool expected)
        {
            var converter = new BooleanConverter();
            converter.ConvertFromString(input, null, null);
        }

        [TestCase("ja", true)]
        [TestCase("yup", true)]
        public void CorrectlyConvertsCustomValue(string input, bool expected)
        {
            var converter = new BooleanConverter();
            converter.TrueValues.Add("JA");
            converter.TrueValues.Add("YUP");

            converter.ConvertFromString(input, null, null);
        }
    }
}