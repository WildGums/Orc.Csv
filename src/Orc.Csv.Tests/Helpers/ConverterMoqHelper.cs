namespace Orc.Csv.Tests;

using CsvHelper.Configuration;
using CsvHelper;
using Moq;

public static class ConverterMoqHelper
{
    public static (IReaderRow ReaderRow, IWriterRow WriterRow, MemberMapData MemberData) CreateMoqConverterParametersSet()
    {
        var rowReaderMoq = new Mock<IReaderRow>().Object;
        var rowWriterMoq = new Mock<IWriterRow>().Object;
        var dummyMemberMapData = new MemberMapData(null);

        return (rowReaderMoq, rowWriterMoq, dummyMemberMapData);
    }
}
