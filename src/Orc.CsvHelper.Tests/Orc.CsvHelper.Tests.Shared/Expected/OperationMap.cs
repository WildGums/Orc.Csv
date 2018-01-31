namespace nameSpace
{
    using CsvHelper.Configuration;

    public sealed class OperationMap : ClassMap<Operation>
    {
        public OperationMap()
        {
                Map(x => x.Id).Name("Id");
                Map(x => x.Name).Name("Name");
                Map(x => x.StartTime).Name("StartTime");
                Map(x => x.Duration).Name("Duration");
                Map(x => x.Quantity).Name("Quantity");
                Map(x => x.Enabled).Name("Enabled");
        }
    }
}
