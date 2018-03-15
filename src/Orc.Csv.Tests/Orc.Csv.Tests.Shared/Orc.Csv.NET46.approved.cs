[assembly: System.Resources.NeutralResourcesLanguageAttribute("en-US")]
[assembly: System.Runtime.InteropServices.ComVisibleAttribute(false)]
[assembly: System.Runtime.Versioning.TargetFrameworkAttribute(".NETFramework,Version=v4.6", FrameworkDisplayName=".NET Framework 4.6")]


public class static ModuleInitializer
{
    public static void Initialize() { }
}
namespace Orc.Csv
{
    
    public class BooleanConverter : Orc.Csv.TypeConverterBase<bool>
    {
        public BooleanConverter() { }
        public BooleanConverter(string[] additionalTrueValues, string[] additionalFalseValues) { }
        public System.Collections.Generic.List<string> FalseValues { get; }
        public System.Collections.Generic.List<string> TrueValues { get; }
        public override object ConvertFromString(string text, CsvHelper.IReaderRow row, CsvHelper.Configuration.MemberMapData memberMapData) { }
    }
    public abstract class ClassMapBase<TRecord> : CsvHelper.Configuration.ClassMap<TRecord>
    
    {
        protected ClassMapBase() { }
    }
    public class static ClassMapExtensions
    {
        public static System.Type GetRecordType(this CsvHelper.Configuration.ClassMap classMap) { }
    }
    public class CsvContext : Orc.Csv.ICsvContext
    {
        public CsvContext(System.Type recordType) { }
        public CsvHelper.Configuration.ClassMap ClassMap { get; set; }
        public CsvHelper.Configuration.Configuration Configuration { get; set; }
        public System.Globalization.CultureInfo Culture { get; set; }
        public System.Action<object> Initializer { get; set; }
        public System.Type RecordType { get; set; }
        public bool ThrowOnError { get; set; }
    }
    public class CsvContext<TRecord> : Orc.Csv.CsvContext
    
    {
        public CsvContext() { }
    }
    public class CsvContext<TRecord, TMap> : Orc.Csv.CsvContext<TRecord>
    
        where TMap : CsvHelper.Configuration.ClassMap, new ()
    {
        public CsvContext() { }
    }
    public class static CsvEnvironment
    {
        public static readonly System.Globalization.CultureInfo DefaultCultureInfo;
        public static readonly System.DateTime ExcelNullDate;
    }
    public class CsvReaderService : Orc.Csv.CsvServiceBase, Orc.Csv.ICsvReaderService
    {
        public CsvReaderService() { }
        public CsvHelper.CsvReader CreateReader(System.IO.StreamReader streamReader, Orc.Csv.ICsvContext csvContext) { }
        protected virtual System.Collections.IEnumerable ReadData(CsvHelper.CsvReader csvReader, Orc.Csv.ICsvContext csvContext) { }
        protected virtual System.Threading.Tasks.Task<System.Collections.IEnumerable> ReadDataAsync(CsvHelper.CsvReader csvReader, Orc.Csv.ICsvContext csvContext) { }
        public virtual System.Collections.IEnumerable ReadRecords(System.IO.StreamReader streamReader, Orc.Csv.ICsvContext csvContext) { }
        public System.Threading.Tasks.Task<System.Collections.IEnumerable> ReadRecordsAsync(System.IO.StreamReader streamReader, Orc.Csv.ICsvContext csvContext) { }
    }
    public abstract class CsvServiceBase
    {
        protected CsvServiceBase() { }
        public virtual CsvHelper.Configuration.Configuration CreateDefaultConfiguration(Orc.Csv.ICsvContext csvContext) { }
        protected virtual CsvHelper.Configuration.Configuration EnsureCorrectConfiguration(CsvHelper.Configuration.Configuration configuration, Orc.Csv.ICsvContext csvContext) { }
    }
    public class CsvWriterService : Orc.Csv.CsvServiceBase, Orc.Csv.ICsvWriterService
    {
        public CsvWriterService() { }
        public virtual CsvHelper.CsvWriter CreateWriter(System.IO.StreamWriter streamWriter, Orc.Csv.ICsvContext csvContext) { }
        public virtual void WriteRecords(System.Collections.IEnumerable records, System.IO.StreamWriter streamWriter, Orc.Csv.ICsvContext csvContext) { }
        protected virtual void WriteRecords(System.Collections.IEnumerable records, CsvHelper.CsvWriter csvWriter, Orc.Csv.ICsvContext csvContext) { }
        public virtual System.Threading.Tasks.Task WriteRecordsAsync(System.Collections.IEnumerable records, System.IO.StreamWriter streamWriter, Orc.Csv.ICsvContext csvContext) { }
    }
    public class DynamicTypeConverter<T> : CsvHelper.TypeConversion.ITypeConverter
    
    {
        public DynamicTypeConverter(System.Func<string, CsvHelper.IReaderRow, CsvHelper.Configuration.MemberMapData, T> convertFromString, System.Func<object, CsvHelper.IWriterRow, CsvHelper.Configuration.MemberMapData, string> convertToString) { }
        public object ConvertFromString(string text, CsvHelper.IReaderRow row, CsvHelper.Configuration.MemberMapData memberMapData) { }
        public string ConvertToString(object value, CsvHelper.IWriterRow row, CsvHelper.Configuration.MemberMapData memberMapData) { }
    }
    public class EnumConverter<T> : Orc.Csv.TypeConverterBase<T>
        where T :  struct, System.IComparable, System.IFormattable
    {
        public EnumConverter() { }
        public EnumConverter(T defaultValue) { }
        public T DefaultValue { get; set; }
        public override object ConvertFromString(string text, CsvHelper.IReaderRow row, CsvHelper.Configuration.MemberMapData memberMapData) { }
    }
    public interface ICsvContext
    {
        CsvHelper.Configuration.ClassMap ClassMap { get; set; }
        CsvHelper.Configuration.Configuration Configuration { get; set; }
        System.Globalization.CultureInfo Culture { get; set; }
        System.Action<object> Initializer { get; set; }
        System.Type RecordType { get; set; }
        bool ThrowOnError { get; set; }
    }
    public interface ICsvReaderService
    {
        CsvHelper.Configuration.Configuration CreateDefaultConfiguration(Orc.Csv.ICsvContext csvContext);
        CsvHelper.CsvReader CreateReader(System.IO.StreamReader streamReader, Orc.Csv.ICsvContext csvContext);
        System.Collections.IEnumerable ReadRecords(System.IO.StreamReader streamReader, Orc.Csv.ICsvContext csvContext);
        System.Threading.Tasks.Task<System.Collections.IEnumerable> ReadRecordsAsync(System.IO.StreamReader streamReader, Orc.Csv.ICsvContext csvContext);
    }
    public class static ICsvReaderServiceExtensions
    {
        public static CsvHelper.CsvReader CreateReader(this Orc.Csv.ICsvReaderService csvReaderService, string fileName, Orc.Csv.ICsvContext csvContext) { }
        public static System.Collections.IEnumerable ReadRecords(this Orc.Csv.ICsvReaderService csvReaderService, string fileName, Orc.Csv.ICsvContext csvContext) { }
        public static System.Collections.Generic.List<TRecord> ReadRecords<TRecord>(this Orc.Csv.ICsvReaderService csvReaderService, string fileName, Orc.Csv.ICsvContext csvContext) { }
        public static System.Collections.Generic.List<TRecord> ReadRecords<TRecord, TRecordMap>(this Orc.Csv.ICsvReaderService csvReaderService, string fileName, Orc.Csv.ICsvContext csvContext = null)
        
            where TRecordMap : CsvHelper.Configuration.ClassMap, new () { }
        public static System.Collections.Generic.List<TRecord> ReadRecords<TRecord>(this Orc.Csv.ICsvReaderService csvReaderService, System.IO.StreamReader streamReader, Orc.Csv.ICsvContext csvContext) { }
        public static System.Collections.Generic.List<TRecord> ReadRecords<TRecord, TRecordMap>(this Orc.Csv.ICsvReaderService csvReaderService, System.IO.StreamReader streamReader, Orc.Csv.ICsvContext csvContext = null)
        
            where TRecordMap : CsvHelper.Configuration.ClassMap, new () { }
        public static System.Threading.Tasks.Task<System.Collections.IEnumerable> ReadRecordsAsync(this Orc.Csv.ICsvReaderService csvReaderService, string fileName, Orc.Csv.ICsvContext csvContext) { }
        public static System.Threading.Tasks.Task<System.Collections.Generic.List<TRecord>> ReadRecordsAsync<TRecord>(this Orc.Csv.ICsvReaderService csvReaderService, string fileName, Orc.Csv.ICsvContext csvContext) { }
        public static System.Threading.Tasks.Task<System.Collections.Generic.List<TRecord>> ReadRecordsAsync<TRecord, TRecordMap>(this Orc.Csv.ICsvReaderService csvReaderService, string fileName, Orc.Csv.ICsvContext csvContext = null)
        
            where TRecordMap : CsvHelper.Configuration.ClassMap, new () { }
        public static System.Threading.Tasks.Task<System.Collections.Generic.List<TRecord>> ReadRecordsAsync<TRecord>(this Orc.Csv.ICsvReaderService csvReaderService, System.IO.StreamReader streamReader, Orc.Csv.ICsvContext csvContext) { }
        public static System.Threading.Tasks.Task<System.Collections.Generic.List<TRecord>> ReadRecordsAsync<TRecord, TRecordMap>(this Orc.Csv.ICsvReaderService csvReaderService, System.IO.StreamReader streamReader, Orc.Csv.ICsvContext csvContext = null)
        
            where TRecordMap : CsvHelper.Configuration.ClassMap, new () { }
    }
    public interface ICsvWriterService
    {
        CsvHelper.Configuration.Configuration CreateDefaultConfiguration(Orc.Csv.ICsvContext csvContext);
        CsvHelper.CsvWriter CreateWriter(System.IO.StreamWriter streamWriter, Orc.Csv.ICsvContext csvContext);
        void WriteRecords(System.Collections.IEnumerable records, System.IO.StreamWriter streamWriter, Orc.Csv.ICsvContext csvContext);
        System.Threading.Tasks.Task WriteRecordsAsync(System.Collections.IEnumerable records, System.IO.StreamWriter streamWriter, Orc.Csv.ICsvContext csvContext);
    }
    public class static ICsvWriterServiceExtensions
    {
        public static CsvHelper.CsvWriter CreateWriter(this Orc.Csv.ICsvWriterService csvWriterService, string fileName, Orc.Csv.ICsvContext csvContext) { }
        public static void WriteRecords(this Orc.Csv.ICsvWriterService csvWriterService, System.Collections.IEnumerable records, string fileName, Orc.Csv.ICsvContext csvContext) { }
        public static void WriteRecords<TRecord, TRecordMap>(this Orc.Csv.ICsvWriterService csvWriterService, System.Collections.Generic.IEnumerable<TRecord> records, string fileName, Orc.Csv.ICsvContext csvContext = null)
        
            where TRecordMap : CsvHelper.Configuration.ClassMap, new () { }
        public static void WriteRecords<TRecord, TRecordMap>(this Orc.Csv.ICsvWriterService csvWriterService, System.Collections.Generic.IEnumerable<TRecord> records, System.IO.StreamWriter streamWriter, Orc.Csv.ICsvContext csvContext = null)
        
            where TRecordMap : CsvHelper.Configuration.ClassMap, new () { }
        public static System.Threading.Tasks.Task WriteRecordsAsync(this Orc.Csv.ICsvWriterService csvWriterService, System.Collections.IEnumerable records, string fileName, Orc.Csv.ICsvContext csvContext) { }
        public static System.Threading.Tasks.Task WriteRecordsAsync<TRecord, TRecordMap>(this Orc.Csv.ICsvWriterService csvWriterService, System.Collections.Generic.IEnumerable<TRecord> records, string fileName, Orc.Csv.ICsvContext csvContext = null)
        
            where TRecordMap : CsvHelper.Configuration.ClassMap, new () { }
        public static System.Threading.Tasks.Task WriteRecordsAsync<TRecord, TRecordMap>(this Orc.Csv.ICsvWriterService csvWriterService, System.Collections.Generic.IEnumerable<TRecord> records, System.IO.StreamWriter streamWriter, Orc.Csv.ICsvContext csvContext = null)
        
            where TRecordMap : CsvHelper.Configuration.ClassMap, new () { }
    }
    public class static MemberMapExtensions
    {
        public static CsvHelper.Configuration.MemberMap AsBool(this CsvHelper.Configuration.MemberMap map) { }
        public static CsvHelper.Configuration.MemberMap AsBool(this CsvHelper.Configuration.MemberMap map, string[] additionalTrueValues, string[] additionalFalseValues) { }
        public static CsvHelper.Configuration.MemberMap AsDateTime(this CsvHelper.Configuration.MemberMap map) { }
        public static CsvHelper.Configuration.MemberMap AsDecimal(this CsvHelper.Configuration.MemberMap map) { }
        public static CsvHelper.Configuration.MemberMap AsDouble(this CsvHelper.Configuration.MemberMap map) { }
        public static CsvHelper.Configuration.MemberMap AsEnum<TEnum>(this CsvHelper.Configuration.MemberMap map, TEnum defaultValue = null)
            where TEnum :  struct, System.IComparable, System.IFormattable { }
        public static CsvHelper.Configuration.MemberMap AsInt(this CsvHelper.Configuration.MemberMap map) { }
        public static CsvHelper.Configuration.MemberMap AsLong(this CsvHelper.Configuration.MemberMap map) { }
        public static CsvHelper.Configuration.MemberMap AsNullableBool(this CsvHelper.Configuration.MemberMap map) { }
        public static CsvHelper.Configuration.MemberMap AsNullableDateTime(this CsvHelper.Configuration.MemberMap map) { }
        public static CsvHelper.Configuration.MemberMap AsNullableDecimal(this CsvHelper.Configuration.MemberMap map) { }
        public static CsvHelper.Configuration.MemberMap AsNullableDouble(this CsvHelper.Configuration.MemberMap map) { }
        public static CsvHelper.Configuration.MemberMap AsNullableInt(this CsvHelper.Configuration.MemberMap map) { }
        public static CsvHelper.Configuration.MemberMap AsNullableLong(this CsvHelper.Configuration.MemberMap map) { }
        public static CsvHelper.Configuration.MemberMap AsNullableShort(this CsvHelper.Configuration.MemberMap map) { }
        public static CsvHelper.Configuration.MemberMap AsNullableTimeSpan(this CsvHelper.Configuration.MemberMap map) { }
        public static CsvHelper.Configuration.MemberMap AsShort(this CsvHelper.Configuration.MemberMap map) { }
        public static CsvHelper.Configuration.MemberMap AsString(this CsvHelper.Configuration.MemberMap map) { }
        public static CsvHelper.Configuration.MemberMap AsTimeSpan(this CsvHelper.Configuration.MemberMap map) { }
    }
    public class NullableBooleanConverter : Orc.Csv.NullableTypeConverterBase<System.Nullable<bool>>
    {
        public NullableBooleanConverter() { }
        public System.Collections.Generic.List<string> FalseValues { get; }
        public System.Collections.Generic.List<string> TrueValues { get; }
        protected override System.Nullable<bool> ConvertStringToActualType(CsvHelper.IReaderRow row, string text) { }
    }
    public class NullableDateTimeConverter : Orc.Csv.NullableTypeConverterBase<System.Nullable<System.DateTime>>
    {
        public NullableDateTimeConverter() { }
        protected override System.Nullable<System.DateTime> ConvertStringToActualType(CsvHelper.IReaderRow row, string text) { }
    }
    public class NullableDecimalConverter : Orc.Csv.NullableTypeConverterBase<System.Nullable<decimal>>
    {
        public NullableDecimalConverter() { }
        protected override System.Nullable<decimal> ConvertStringToActualType(CsvHelper.IReaderRow row, string text) { }
    }
    public class NullableDoubleConverter : Orc.Csv.NullableTypeConverterBase<System.Nullable<double>>
    {
        public NullableDoubleConverter() { }
        protected override System.Nullable<double> ConvertStringToActualType(CsvHelper.IReaderRow row, string text) { }
    }
    public class NullableIntConverter : Orc.Csv.NullableTypeConverterBase<System.Nullable<int>>
    {
        public NullableIntConverter() { }
        protected override System.Nullable<int> ConvertStringToActualType(CsvHelper.IReaderRow row, string text) { }
    }
    public class NullableLongConverter : Orc.Csv.NullableTypeConverterBase<System.Nullable<long>>
    {
        public NullableLongConverter() { }
        protected override System.Nullable<long> ConvertStringToActualType(CsvHelper.IReaderRow row, string text) { }
    }
    public class NullableShortConverter : Orc.Csv.NullableTypeConverterBase<System.Nullable<short>>
    {
        public NullableShortConverter() { }
        protected override System.Nullable<short> ConvertStringToActualType(CsvHelper.IReaderRow row, string text) { }
    }
    public class NullableTimeSpanConverter : Orc.Csv.NullableTypeConverterBase<System.Nullable<System.TimeSpan>>
    {
        public NullableTimeSpanConverter() { }
        protected override System.Nullable<System.TimeSpan> ConvertStringToActualType(CsvHelper.IReaderRow row, string text) { }
    }
    public abstract class NullableTypeConverterBase<TNullable> : Orc.Csv.TypeConverterBase<TNullable>
    
    {
        protected NullableTypeConverterBase() { }
        public override object ConvertFromString(string text, CsvHelper.IReaderRow row, CsvHelper.Configuration.MemberMapData memberMapData) { }
        protected abstract TNullable ConvertStringToActualType(CsvHelper.IReaderRow row, string text);
    }
    public class NullableUIntConverter : Orc.Csv.NullableTypeConverterBase<System.Nullable<uint>>
    {
        public NullableUIntConverter() { }
        protected override System.Nullable<uint> ConvertStringToActualType(CsvHelper.IReaderRow row, string text) { }
    }
    public class NullableULongConverter : Orc.Csv.NullableTypeConverterBase<System.Nullable<ulong>>
    {
        public NullableULongConverter() { }
        protected override System.Nullable<ulong> ConvertStringToActualType(CsvHelper.IReaderRow row, string text) { }
    }
    public class NullableUShortConverter : Orc.Csv.NullableTypeConverterBase<System.Nullable<ushort>>
    {
        public NullableUShortConverter() { }
        protected override System.Nullable<ushort> ConvertStringToActualType(CsvHelper.IReaderRow row, string text) { }
    }
    public class static StringExtensions
    {
        public static string ToCamelCase(this string input) { }
    }
    public abstract class TypeConverterBase<T> : CsvHelper.TypeConversion.ITypeConverter
    
    {
        protected TypeConverterBase() { }
        public abstract object ConvertFromString(string text, CsvHelper.IReaderRow row, CsvHelper.Configuration.MemberMapData memberMapData);
        public virtual string ConvertToString(object value, CsvHelper.IWriterRow row, CsvHelper.Configuration.MemberMapData memberMapData) { }
        protected System.Globalization.CultureInfo GetCultureInfo(CsvHelper.IWriterRow row) { }
        protected System.Globalization.CultureInfo GetCultureInfo(CsvHelper.IReaderRow row) { }
    }
}