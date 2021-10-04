using EngieRegexService.Api.Entities;
using EngieRegexService.Api.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EngieRegexService.Api.Services
{
    public class RegexHandler: IRegexHandler
    {
        public string Substitute(string pattern, string input, string substitution, RegexOptions regexOptions = RegexOptions.None)
        {
            //Guard.ThrowIfNullOrEmpty(input, "Please enter the input", "input");
            if (string.IsNullOrWhiteSpace(pattern))
                throw new ValidationException("Please enter the regex pattern", pattern);
            if (string.IsNullOrWhiteSpace(input))
                throw new ValidationException("Please enter a text", input);
            if (string.IsNullOrWhiteSpace(substitution))
                throw new ValidationException("Please enter a substitution", substitution);

            try
            {
                var regex = new Regex(pattern, regexOptions);
                var result = regex.Replace(input, substitution);

                return result;
            }
            catch (RegexMatchTimeoutException e)
            {
                throw new RegexMatchTimeoutException("Substitution timed out! - Timeout interval specified: " + e.MatchTimeout +
                    "- Pattern: " + e.Pattern + "- Input: " + e.Input);
            }
        }

        public ResponseObject Match(string pattern, string input, RegexOptions regexOptions = RegexOptions.None)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                throw new ValidationException("Please enter the regex pattern", pattern);
            if (string.IsNullOrWhiteSpace(input))
                throw new ValidationException("Please enter a text", input);

            RegexOptions options = RegexOptions.None;
            if (regexOptions != RegexOptions.None)
                options = regexOptions;
            try
            {
                var isMatch = Regex.IsMatch(input, pattern, options);
                if (!isMatch)
                    return new ResponseObject();

                //Find successful matches
                var matches = Regex.Matches(input, pattern, options);

                var result = new ResponseObject();
                result.IsMatch = isMatch;
                result.MatchesCount = matches.Count;

                var matchesValueIndex = new Dictionary<int, string>();
                foreach (Match m in Regex.Matches(input, pattern, options))
                {
                    matchesValueIndex.Add(m.Index, m.Value);
                }
                result.ResultList = matchesValueIndex.ToList();

                return result;
            }
            catch (RegexMatchTimeoutException e)
            {
                throw new RegexMatchTimeoutException("Match timed out! - Timeout interval specified: " + e.MatchTimeout +
                    "- Pattern: " + e.Pattern + "- Input: " + e.Input);
            }
        }
    }
}
