namespace Orc.Csv.Tests.Converters;

using Csv;
using CsvHelper;
using Moq;
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
        var rowReaderMoq = new Mock<IReaderRow>().Object;

        var converter = new BooleanConverter();
        var result = converter.ConvertFromString(input, rowReaderMoq, null);

        Assert.AreEqual(expected, result);
    }

    [TestCase("ja", true)]
    [TestCase("yup", true)]
    [TestCase("ups", false)]
    public void CorrectlyConvertsCustomValue(string input, bool expected)
    {
        var rowReaderMoq = new Mock<IReaderRow>().Object;

        var converter = new BooleanConverter().AddTrueValues("JA", "YUP");
        var result = converter.ConvertFromString(input, rowReaderMoq, null);

        Assert.AreEqual(expected, result);
    }
}
