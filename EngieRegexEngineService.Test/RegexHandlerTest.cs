using EngieRegexService.Api.Entities;
using EngieRegexService.Api.Services;
using EngieRegexService.Api.Utilities;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xunit;

namespace EngieRegexEngineService.Api.Test
{
    public class RegexHandlerTest
    {
        private readonly IRegexHandler _regexHandler;
        public RegexHandlerTest()
        {
            _regexHandler = new RegexHandler();
        }

        [Fact]
        public void MatchRegex_WhenInvalidPatternIsEmpty_ShouldGenerateError()
        {
            // Arrange
            string input = "abc";
            string pattern = "";

            // Act & Assert
            var ex = Assert.Throws<ValidationException>(() => _regexHandler.Match(pattern, input));
            Assert.Equal("Please enter the regex pattern", ex.Message);
        }

        [Fact]
        public void MatchRegex_WhenInvalidPatternIsNull_ShouldGenerateError()
        {
            // Arrange
            string input = "abc";
            string pattern = null;

            // Act & Assert
            var ex = Assert.Throws<ValidationException>(() => _regexHandler.Match(pattern, input));
            Assert.Equal("Please enter the regex pattern", ex.Message);
        }

        [Fact]
        public void MatchRegex_WhenInvalidInputIsEmpty_ShouldGenerateError()
        {
            // Arrange
            string input = "";
            string pattern = "[abc]";

            // Act & Assert
            var ex = Assert.Throws<ValidationException>(() => _regexHandler.Match(pattern, input));
            Assert.Equal("Please enter a text", ex.Message);
        }

        [Fact]
        public void MatchRegex_WhenInvalidInputIsNull_ShouldGenerateError()
        {
            // Arrange
            string input = null;
            string pattern = "[abc]";

            // Act & Assert
            var ex = Assert.Throws<ValidationException>(() => _regexHandler.Match(pattern, input));
            Assert.Equal("Please enter a text", ex.Message);
        }

        [Fact]
        public void ReplaceRegex_WhenInvalidSubstitutionIsEmpty_ShouldGenerateError()
        {
            // Arrange
            string input = "abc";
            string pattern = "[abc]";
            string substitution = "";

            // Act & Assert
            var ex = Assert.Throws<ValidationException>(() => _regexHandler.Substitute(pattern, input, substitution));
            Assert.Equal("Please enter a substitution", ex.Message);
        }

        [Fact]
        public void ReplaceRegex_WhenInvalidSubstitutionIsNull_ShouldGenerateError()
        {
            // Arrange
            string input = "abc";
            string pattern = "[abc]";
            string substitution = null;

            // Act & Assert
            var ex = Assert.Throws<ValidationException>(() => _regexHandler.Substitute(pattern, input, substitution));
            Assert.Equal("Please enter a substitution", ex.Message);
        }

        [Fact]
        public void MatchRegex_WhenValidInputAndPatternIsMatchShouldBeTrueWithCorrectMatchesCount()
        {
            //Arrange
            string input = "abc";
            string pattern = "[abc]";

            var ExpectedResponse = GivenResponseWhenPatternAndInputAreValid();

            //Act & Assert
            var actualResponse = _regexHandler.Match(pattern, input, RegexOptions.None);
            Assert.Equal(ExpectedResponse.IsMatch, actualResponse.IsMatch);
            Assert.Equal(ExpectedResponse.MatchesCount, actualResponse.MatchesCount);
            Assert.Equal(ExpectedResponse.ResultList.Count, actualResponse.ResultList.Count);
            Assert.Equal(ExpectedResponse.ResultList[0], actualResponse.ResultList[0]);
            Assert.Equal(ExpectedResponse.ResultList[1], actualResponse.ResultList[1]);
            Assert.Equal(ExpectedResponse.ResultList[2], actualResponse.ResultList[2]);
        }

        [Fact]
        public void ReplaceRegex_WhenValidInputAndPatternAndSubstitutionAreValids()
        {
            //Arrange
            string input = "abc";
            string pattern = "[abc]";
            string substitution = "s";

            var ExpectedResponse = "sss";

            //Act & Assert
            var actualResponse = _regexHandler.Substitute(pattern, input, substitution, RegexOptions.None);
            Assert.Equal(ExpectedResponse, actualResponse);
        }

        [Fact]
        public void MatchRegex_WhenValidInputAndPatternWithMultilineOpIsMatchShouldBeTrueWithCorrectMatchesCount()
        {
            //Arrange
            string input = "abc";
            string pattern = "[abc]";

            var ExpectedResponse = GivenResponseWhenPatternAndInputAreValid();

            //Act & Assert
            var actualResponse = _regexHandler.Match(pattern, input, RegexOptions.Multiline);
            Assert.Equal(ExpectedResponse.IsMatch, actualResponse.IsMatch);
            Assert.Equal(ExpectedResponse.MatchesCount, actualResponse.MatchesCount);
            Assert.Equal(ExpectedResponse.ResultList.Count, actualResponse.ResultList.Count);
            Assert.Equal(ExpectedResponse.ResultList[0], actualResponse.ResultList[0]);
            Assert.Equal(ExpectedResponse.ResultList[1], actualResponse.ResultList[1]);
            Assert.Equal(ExpectedResponse.ResultList[2], actualResponse.ResultList[2]);
        }
        private ResponseObject GivenResponseWhenPatternAndInputAreValid()
        {
            return  new ResponseObject
            {
                IsMatch = true,
                MatchesCount = 3,
                ResultList = new List<KeyValuePair<int, string>>()
                                {
                                    new KeyValuePair<int, string>(0,"a"),
                                    new KeyValuePair<int, string>(1,"b"),
                                    new KeyValuePair<int, string>(2,"c")
                                }
            };
        }

        //Add tests for other flags
    }
}
