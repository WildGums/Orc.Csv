namespace Orc.Csv.Test.CsvMaps
{
    using Csv.Test.Entities;
    using global::CsvHelper.Configuration;

    public class OperationCsvMap : CsvClassMap<Operation>
    {
        public OperationCsvMap()
        {
            this.Map(x => x.Id).Name("Id");
            this.Map(x => x.Name).Name("Name");
            this.Map(x => x.StartTime).Name("StartTime");
            this.Map(x => x.Duration).Name("Duration");
            this.Map(x => x.Quantity).Name("Quantity");
            this.Map(x => x.Enabled).Name("Enabled");
        }
    }
}