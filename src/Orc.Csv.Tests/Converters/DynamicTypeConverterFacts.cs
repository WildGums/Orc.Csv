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
        Assert.That(result, Is.EqualTo(expectedDynamicTypeConverterClassInstance));

        var convertBackResult = dynamicTypeConverter.ConvertToString(expectedDynamicTypeConverterClassInstance, moq.WriterRow, moq.MemberData);
        Assert.That(convertBackResult, Is.EqualTo(ExpectedString));
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
        Assert.That(result.Value, Is.EqualTo(defaultValue));
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
        Assert.That(result, Is.EqualTo(defaultValue));
    }

    public class DynamicTypeConverterClass
    {
        public string Value { get; set; }
    }
}
