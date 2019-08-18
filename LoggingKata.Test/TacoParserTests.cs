using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Theory]
        [InlineData("34.073638,-84.677017,Taco Bell Acwort... (Free trial * Add to Cart for a full POI info)")]
        [InlineData("34.035985,-84.683302,Taco Bell Acworth/... (Free trial * Add to Cart for a full POI info)")]
        [InlineData("90,180,Taco Bell")]
        [InlineData("-90,-180,Taco Bell")]
        [InlineData("0, 0, Taco Bell")]
        [InlineData("0 ,0 ,Taco Bell ")]
        public void ShouldParse(string str)
        {
            //Arrange
            TacoParser parser = new TacoParser();

            //Act (call the parse method from csv file)
            ITrackable actual = parser.Parse(str);

            //Assert
            Assert.NotNull(actual);
            Assert.NotNull(actual.Name);
            Assert.NotNull(actual.Location);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("10, 10")]
        [InlineData("10, Taco Bell")]
        [InlineData("10, Ten, Taco Bell")]
        [InlineData("Ten, 10, Taco Bell")]
        [InlineData("10, , Taco Bell")]
        [InlineData(", 10, Taco Bell")]
        [InlineData("10, 10, ")]
        public void ShouldFailParse(string str)
        {
            //Arrange
            TacoParser parser = new TacoParser();

            //Act
            ITrackable actual = parser.Parse(str);

            //Assert
            Assert.Null(actual);


        }
    }
}
