[assembly: System.Resources.NeutralResourcesLanguageAttribute("en-US")]
[assembly: System.Runtime.InteropServices.ComVisibleAttribute(false)]
[assembly: System.Runtime.Versioning.TargetFrameworkAttribute(".NETFramework,Version=v4.5", FrameworkDisplayName=".NET Framework 4.5")]


public class static ModuleInitializer
{
    public static void Initialize() { }
}
namespace Orc.Csv
{
    
    public class CodeGenerationService : Orc.Csv.ICodeGenerationService
    {
        public CodeGenerationService(Orc.Csv.IEntityPluralService entityPluralService, Orc.FileSystem.IFileService fileService, Orc.FileSystem.IDirectoryService directoryService, Orc.Csv.ICsvReaderService csvReaderService) { }
        public void CreateCSharpFiles(string csvFilePath, string namespaceName, string outputFolder) { }
        public void CreateCSharpFilesForAllCsvFiles(string inputFoler, string namespaceName, string outputFolder) { }
        public string[] GetCsvFiles(string folderPath) { }
    }
    public class static CsvEnvironment
    {
        public static readonly System.Globalization.CultureInfo DefaultCultureInfo;
        public static readonly System.DateTime ExcelNullDate;
    }
    public class CsvReaderService : Orc.Csv.ICsvReaderService
    {
        public CsvReaderService(Orc.FileSystem.IFileService fileService) { }
        protected virtual CsvHelper.CsvReader CreateCsvReader(string csvFilePath, CsvHelper.Configuration.CsvConfiguration csvConfiguration) { }
        protected virtual CsvHelper.Configuration.CsvConfiguration CreateDefaultCsvConfiguration(System.Globalization.CultureInfo culture) { }
        public CsvHelper.CsvReader CreateReader(string csvFilePath, CsvHelper.Configuration.CsvClassMap csvMap, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, System.Globalization.CultureInfo culture = null) { }
        public virtual System.Collections.Generic.IEnumerable<T> ReadCsv<T>(string csvFilePath, CsvHelper.Configuration.CsvClassMap csvMap, System.Action<T> initializer = null, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, bool throwOnError = True, System.Globalization.CultureInfo culture = null) { }
        public System.Threading.Tasks.Task<System.Collections.Generic.IList<T>> ReadCsvAsync<T>(string csvFilePath, CsvHelper.Configuration.CsvClassMap csvMap, System.Action<T> initializer = null, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, bool throwOnError = True, System.Globalization.CultureInfo culture = null) { }
        protected virtual System.Collections.Generic.IEnumerable<T> ReadData<T>(string csvFilePath, System.Action<T> initializer, bool throwOnError, CsvHelper.CsvReader csvReader) { }
    }
    public class CsvValidationService : Orc.Csv.ICsvValidationService
    {
        public CsvValidationService(Orc.Csv.IEntityPluralService pluralService, Orc.Csv.ICsvReaderService csvReaderService) { }
        public Catel.Data.IValidationContext Validate(string csvFilePath) { }
        public Catel.Data.IValidationContext Validate(string csvFilePath, string className, System.Collections.Generic.IEnumerable<string> propertyNames) { }
    }
    public class CsvWriterService : Orc.Csv.ICsvWriterService
    {
        public CsvWriterService(Orc.FileSystem.IFileService fileService) { }
        public virtual CsvHelper.Configuration.CsvConfiguration CreateDefaultCsvConfiguration(System.Globalization.CultureInfo cultureInfo = null) { }
        public virtual CsvHelper.CsvWriter CreateWriter(string csvFilePath, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null) { }
        public virtual CsvHelper.CsvWriter CreateWriter(System.IO.StreamWriter streamWriter, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null) { }
        public virtual void WriteCsv(System.Collections.IEnumerable records, string csvFilePath, System.Type recordType, CsvHelper.Configuration.CsvClassMap csvMap, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, bool throwOnError = False, System.Globalization.CultureInfo cultureInfo = null) { }
        public virtual void WriteCsv(System.Collections.IEnumerable records, System.IO.StreamWriter streamWriter, System.Type recordType, CsvHelper.Configuration.CsvClassMap csvMap, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, bool throwOnError = False, System.Globalization.CultureInfo cultureInfo = null) { }
        public virtual System.Threading.Tasks.Task WriteCsvAsync(System.Collections.IEnumerable records, string csvFilePath, System.Type recordType, CsvHelper.Configuration.CsvClassMap csvMap = null, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, bool throwOnError = False, System.Globalization.CultureInfo cultureInfo = null) { }
        public virtual void WriteRecord(CsvHelper.CsvWriter writer, params object[] fields) { }
        protected virtual void WriteRecords(System.Collections.IEnumerable records, System.Type recordType, bool throwOnError, CsvHelper.CsvWriter csvWriter) { }
    }
    public class EntityPluralService : Orc.Csv.IEntityPluralService
    {
        public EntityPluralService() { }
        public string ToPlural(string entity) { }
        public string ToSingular(string entity) { }
    }
    public class EnumConverter<T> : CsvHelper.TypeConversion.ITypeConverter
        where T :  struct, System.IComparable, System.IFormattable
    {
        public EnumConverter() { }
        public EnumConverter(T defaultEnumValue) { }
        public bool CanConvertFrom(System.Type type) { }
        public bool CanConvertTo(System.Type type) { }
        public object ConvertFromString(CsvHelper.TypeConversion.TypeConverterOptions options, string text) { }
        public string ConvertToString(CsvHelper.TypeConversion.TypeConverterOptions options, object value) { }
    }
    public interface ICodeGenerationService
    {
        void CreateCSharpFiles(string csvFilePath, string namespaceName, string outputFolder);
        void CreateCSharpFilesForAllCsvFiles(string inputFoler, string namespaceName, string outputFolder);
        string[] GetCsvFiles(string folderPath);
    }
    public interface ICsvReaderService
    {
        CsvHelper.CsvReader CreateReader(string csvFilePath, CsvHelper.Configuration.CsvClassMap csvMap, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, System.Globalization.CultureInfo culture = null);
        System.Collections.Generic.IEnumerable<T> ReadCsv<T>(string csvFilePath, CsvHelper.Configuration.CsvClassMap csvMap, System.Action<T> initializer = null, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, bool throwOnError = True, System.Globalization.CultureInfo culture = null);
        System.Threading.Tasks.Task<System.Collections.Generic.IList<T>> ReadCsvAsync<T>(string csvFilePath, CsvHelper.Configuration.CsvClassMap csvMap, System.Action<T> initializer = null, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, bool throwOnError = True, System.Globalization.CultureInfo culture = null);
    }
    public class static ICsvReaderServiceExtensions
    {
        public static CsvHelper.CsvReader CreateReader(this Orc.Csv.ICsvReaderService csvReaderService, string csvFilePath, System.Type csvMapType = null, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, System.Globalization.CultureInfo culture = null) { }
        public static System.Collections.Generic.IEnumerable<T> ReadCsv<T, TMap>(this Orc.Csv.ICsvReaderService csvReaderService, string csvFilePath, System.Action<T> initializer = null, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, bool throwOnError = True, System.Globalization.CultureInfo culture = null)
        
            where TMap : CsvHelper.Configuration.CsvClassMap { }
        public static System.Collections.Generic.IEnumerable<T> ReadCsv<T>(this Orc.Csv.ICsvReaderService csvReaderService, string csvFilePath, System.Action<T> initializer = null, System.Type csvMapType = null, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, bool throwOnError = True, System.Globalization.CultureInfo culture = null) { }
        public static System.Threading.Tasks.Task<System.Collections.Generic.IList<T>> ReadCsvAsync<T, TMap>(this Orc.Csv.ICsvReaderService csvReaderService, string csvFilePath, System.Action<T> initializer = null, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, bool throwOnError = True, System.Globalization.CultureInfo culture = null)
        
            where TMap : CsvHelper.Configuration.CsvClassMap { }
        public static System.Threading.Tasks.Task<System.Collections.Generic.IList<T>> ReadCsvAsync<T>(this Orc.Csv.ICsvReaderService csvReaderService, string csvFilePath, System.Type csvMapType = null, System.Action<T> initializer = null, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, bool throwOnError = True, System.Globalization.CultureInfo culture = null) { }
    }
    public interface ICsvValidationService
    {
        Catel.Data.IValidationContext Validate(string csvFilePath);
        Catel.Data.IValidationContext Validate(string csvFilePath, string className, System.Collections.Generic.IEnumerable<string> propertyNames);
    }
    public interface ICsvWriterService
    {
        CsvHelper.Configuration.CsvConfiguration CreateDefaultCsvConfiguration(System.Globalization.CultureInfo cultureInfo = null);
        CsvHelper.CsvWriter CreateWriter(string csvFilePath, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null);
        CsvHelper.CsvWriter CreateWriter(System.IO.StreamWriter streamWriter, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null);
        void WriteCsv(System.Collections.IEnumerable records, System.IO.StreamWriter streamWriter, System.Type recordType, CsvHelper.Configuration.CsvClassMap csvMap, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, bool throwOnError = False, System.Globalization.CultureInfo cultureInfo = null);
        void WriteCsv(System.Collections.IEnumerable records, string csvFilePath, System.Type recordType, CsvHelper.Configuration.CsvClassMap csvMap, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, bool throwOnError = False, System.Globalization.CultureInfo cultureInfo = null);
        System.Threading.Tasks.Task WriteCsvAsync(System.Collections.IEnumerable records, string csvFilePath, System.Type recordType, CsvHelper.Configuration.CsvClassMap csvMap, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, bool throwOnError = False, System.Globalization.CultureInfo cultureInfo = null);
        void WriteRecord(CsvHelper.CsvWriter writer, params object[] fields);
    }
    public class static ICsvWriterServiceExtensions
    {
        public static void WriteCsv<TRecord>(this Orc.Csv.ICsvWriterService csvWriterService, System.Collections.Generic.IEnumerable<TRecord> records, string csvFilePath, System.Type csvMap = null, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, bool throwOnError = False, System.Globalization.CultureInfo cultureInfo = null) { }
        public static void WriteCsv<TRecord>(this Orc.Csv.ICsvWriterService csvWriterService, System.Collections.Generic.IEnumerable<TRecord> records, System.IO.StreamWriter streamWriter, System.Type csvMap = null, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, bool throwOnError = False, System.Globalization.CultureInfo cultureInfo = null) { }
        public static void WriteCsv<TRecord>(this Orc.Csv.ICsvWriterService csvWriterService, System.Collections.Generic.IEnumerable<TRecord> records, string csvFilePath, CsvHelper.Configuration.CsvClassMap csvMap, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, bool throwOnError = False, System.Globalization.CultureInfo cultureInfo = null) { }
        public static void WriteCsv<TRecord, TMap>(this Orc.Csv.ICsvWriterService csvWriterService, System.Collections.Generic.IEnumerable<TRecord> records, string csvFilePath, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, bool throwOnError = False, System.Globalization.CultureInfo cultureInfo = null)
        
            where TMap : CsvHelper.Configuration.CsvClassMap { }
        public static System.Threading.Tasks.Task WriteCsvAsync<TRecord>(this Orc.Csv.ICsvWriterService csvWriterService, System.Collections.Generic.IEnumerable<TRecord> records, string csvFilePath, System.Type csvMap = null, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, bool throwOnError = False, System.Globalization.CultureInfo cultureInfo = null) { }
        public static System.Threading.Tasks.Task WriteCsvAsync<TRecord, TMap>(this Orc.Csv.ICsvWriterService csvWriterService, System.Collections.Generic.IEnumerable<TRecord> records, string csvFilePath, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, bool throwOnError = False, System.Globalization.CultureInfo cultureInfo = null)
        
            where TMap : CsvHelper.Configuration.CsvClassMap { }
        public static System.Threading.Tasks.Task WriteCsvAsync<TRecord>(this Orc.Csv.ICsvWriterService csvWriterService, System.Collections.Generic.IEnumerable<TRecord> records, string csvFilePath, CsvHelper.Configuration.CsvClassMap csvMap, CsvHelper.Configuration.CsvConfiguration csvConfiguration = null, bool throwOnError = False, System.Globalization.CultureInfo cultureInfo = null) { }
    }
    public interface IEntityPluralService
    {
        string ToPlural(string entity);
        string ToSingular(string entity);
    }
    public class static StringExtensions
    {
        public static string ToCamelCase(this string input) { }
    }
    public class StringToNullableDateTimeConverter : Orc.Csv.StringToNullableTypeConverterBase<System.Nullable<System.DateTime>>
    {
        public StringToNullableDateTimeConverter() { }
        protected override System.Nullable<System.DateTime> ConvertStringToActualType(CsvHelper.TypeConversion.TypeConverterOptions options, string text) { }
    }
    public class StringToNullableDoubleConverter : Orc.Csv.StringToNullableTypeConverterBase<System.Nullable<double>>
    {
        public StringToNullableDoubleConverter() { }
        protected override System.Nullable<double> ConvertStringToActualType(CsvHelper.TypeConversion.TypeConverterOptions options, string text) { }
    }
    public abstract class StringToNullableTypeConverterBase<TNullable> : Orc.Csv.TypeConverterBase<TNullable>
    
    {
        protected StringToNullableTypeConverterBase() { }
        public override object ConvertFromString(CsvHelper.TypeConversion.TypeConverterOptions options, string text) { }
        protected abstract TNullable ConvertStringToActualType(CsvHelper.TypeConversion.TypeConverterOptions options, string text);
    }
    public class TypeConverter<T> : CsvHelper.TypeConversion.ITypeConverter
    
    {
        public TypeConverter(System.Func<string, T> convertFromString) { }
        public TypeConverter(System.Func<string, string, T> convertFromStringWithDefaultValue, string defaultInput) { }
        public bool CanConvertFrom(System.Type type) { }
        public bool CanConvertTo(System.Type type) { }
        public object ConvertFromString(CsvHelper.TypeConversion.TypeConverterOptions options, string text) { }
        public string ConvertToString(CsvHelper.TypeConversion.TypeConverterOptions options, object value) { }
    }
    public abstract class TypeConverterBase<T> : CsvHelper.TypeConversion.ITypeConverter
    
    {
        public TypeConverterBase() { }
        public bool CanConvertFrom(System.Type type) { }
        public bool CanConvertTo(System.Type type) { }
        public abstract object ConvertFromString(CsvHelper.TypeConversion.TypeConverterOptions options, string text);
        public virtual string ConvertToString(CsvHelper.TypeConversion.TypeConverterOptions options, object value) { }
    }
    public class YesNoToBooleanConverter : Orc.Csv.TypeConverter<bool>
    {
        public YesNoToBooleanConverter() { }
        public YesNoToBooleanConverter(string defaultInput) { }
    }
}