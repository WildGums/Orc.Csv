namespace Orc.Csv.Tests.Converters;

using NUnit.Framework;
using static ConverterMoqHelper;

[TestFixture]
public class DynamicTypeConverterFacts
{
    private const string ExpectedString = "I'm the right one!";

    [Test]
    public void ConvertFromString_And_Convert_ShouldReturnExpectedResult()
    {
        var expectedDynamicTypeConverterClassInstance = new DynamicTypeConverterClass
        {
            Value = ExpectedString
        };

        var moq = CreateMoqConverterParametersSet();
        var dynamicTypeConverter = new DynamicTypeConverter<DynamicTypeConverterClass>(
            (_, _, _) => expectedDynamicTypeConverterClassInstance,
            (o, _, _) => ((DynamicTypeConverterClass)o).Value);

        var result = dynamicTypeConverter.ConvertFromString("Any string", moq.ReaderRow, moq.MemberData);
        Assert.AreEqual(expectedDynamicTypeConverterClassInstance, result);

        var convertBackResult = dynamicTypeConverter.ConvertToString(expectedDynamicTypeConverterClassInstance, moq.WriterRow, moq.MemberData);
        Assert.AreEqual(ExpectedString, convertBackResult);
    }

    [Test]
    public void ConvertFromString_OnNullInput_ShouldReturnDefaultValue()
    {
        const string defaultValue = "default value";

        var moq = CreateMoqConverterParametersSet();
        var dynamicTypeConverter = new DynamicTypeConverter<DynamicTypeConverterClass>(
            (s, _, _) => new DynamicTypeConverterClass {Value = s},
            (o, _, _) => ((DynamicTypeConverterClass)o).Value,
            defaultValue);

        var result = (DynamicTypeConverterClass) dynamicTypeConverter.ConvertFromString(null, moq.ReaderRow, moq.MemberData);
        Assert.AreEqual(defaultValue, result.Value);
    }

    [Test]
    public void ConvertToString_OnNullInput_ShouldReturnDefaultValue()
    {
        const string defaultValue = "default value";

        var moq = CreateMoqConverterParametersSet();
        var dynamicTypeConverter = new DynamicTypeConverter<DynamicTypeConverterClass>(
            (s, _, _) => new DynamicTypeConverterClass { Value = s },
            (o, _, _) => ((DynamicTypeConverterClass)o).Value,
            defaultValue);

        var result = dynamicTypeConverter.ConvertToString(null, moq.WriterRow, moq.MemberData);
        Assert.AreEqual(defaultValue, result);
    }

    public class DynamicTypeConverterClass
    {
        public string Value { get; set; }
    }
}
