namespace Orc.Csv.Tests;

using System;

public class UserDefinedFieldType
{
    public UserDefinedFieldType(string filePath, string fieldName, string type)
    {
        FilePath = filePath;
        FieldName = fieldName;
        Type = type;
    }

    public string FilePath { get; }
    public string FieldName { get; }
    public string Type { get; }
}
