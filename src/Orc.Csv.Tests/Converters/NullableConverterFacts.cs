namespace Orc.Csv.Tests.Converters
{
    using NUnit.Framework;

    [TestFixture]
    public class NullableConverterFacts
    {
        [TestCase("NULL", "NULL", false)]
        [TestCase("NULL", null, true)]
        public void SupportsNullableText(string input, string expectedOutput, bool supportNullText)
        {
            var stringConverter = new NullableStringConverter
            {
                SupportNullText = supportNullText
            };

            var actualOuput = stringConverter.ConvertFromString(input, null, null);

            Assert.AreEqual(expectedOutput, actualOuput);
        }
    }
}
