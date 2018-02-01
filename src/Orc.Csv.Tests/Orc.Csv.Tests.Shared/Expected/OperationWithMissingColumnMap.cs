namespace nameSpace
{
    using CsvHelper.Configuration;

    public sealed class OperationWithMissingColumnMap : ClassMap<OperationWithMissingColumn>
    {
        public OperationWithMissingColumnMap()
        {
                Map(x => x.Name).Name("Name");
                Map(x => x.StartTime).Name("StartTime");
                Map(x => x.Duration).Name("Duration");
        }
    }
}
