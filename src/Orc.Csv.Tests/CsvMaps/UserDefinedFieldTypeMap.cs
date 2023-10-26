namespace Orc.Csv.Tests.CsvMaps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CsvHelper.Configuration;

    public class UserDefinedFieldTypeMap : ClassMap<UserDefinedFieldType>
    {
        public UserDefinedFieldTypeMap()
        {
            Map(x => x.FilePath);
            Map(x => x.FieldName);
            Map(x => x.Type);//.TypeConverter<SystemTypeConverter>();
        }
    }
}
