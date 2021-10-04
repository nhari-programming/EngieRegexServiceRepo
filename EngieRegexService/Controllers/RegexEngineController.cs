using EngieRegexService.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.RegularExpressions;

namespace EngieRegexService.Controllers
{
    [ApiController]
    [Route("api/RegexEngine")]
    public class RegexEngineController : Controller
    {
        private readonly IRegexHandler _regexHandler;

        public RegexEngineController(IRegexHandler regexHandler)
        {
            _regexHandler = regexHandler ?? throw new ArgumentNullException();
        }

        [HttpGet("Match")]
        public IActionResult Match(string regex, string text, RegexOptions regexOptions = RegexOptions.None)
        {
            return new JsonResult(_regexHandler.Match(regex,text));
        }

        [HttpGet("Substitute")]
        public IActionResult Substitute(string pattern, string input, string substitution, RegexOptions regexOptions = RegexOptions.None)
        {
            var result = _regexHandler.Substitute(pattern,input,substitution,regexOptions);

            return new JsonResult(result);
        }       
    }
}
