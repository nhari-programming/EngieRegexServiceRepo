using System.Collections.Generic;

namespace EngieRegexService.Api.Entities
{
    public class ResponseObject
    {
        public ResponseObject()
        {
            IsMatch = false;
            Message = string.Empty;
            MatchesCount = 0;
            ResultList = new List<KeyValuePair<int, string>>();
        }

        public bool IsMatch { get; set; }
        public int MatchesCount { get; set; }
        public string Message { get; set; }
        public List<KeyValuePair<int,string>> ResultList { get; set; }
    }
}
