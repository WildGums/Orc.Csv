namespace Orc.Csv.Tests.Converters;

using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;

public class CustomAttributesTypeConverter : TypeConverterBase
{
    private readonly string _customAttribute;

    public CustomAttributesTypeConverter(string customAttribute)
    {
        _customAttribute = customAttribute;
    }

    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        if (value is not Dictionary<string, string> dictionary)
        {
            return string.Empty;
        }

        var result = dictionary[_customAttribute];
        return result ?? string.Empty;
    }

    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        throw new System.NotImplementedException();
    }
}
