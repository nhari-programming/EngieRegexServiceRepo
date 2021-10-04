using EngieRegexService.Api.Entities;
using System.Text.RegularExpressions;

namespace EngieRegexService.Api.Services
{
    public interface IRegexHandler
    {
        string Substitute(string pattern, string input, string substitution, RegexOptions regexOptions = RegexOptions.None);
        ResponseObject Match(string pattern, string input, RegexOptions regexOptions = RegexOptions.None);
    }
}
