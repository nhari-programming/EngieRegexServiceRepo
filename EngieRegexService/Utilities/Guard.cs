using System;

namespace EngieRegexService.Api.Utilities
{
    public static class Guard
    {
        public static void ThrowIfNullOrEmpty(string argumentValue, string message, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(argumentValue)) throw new ValidationException(message, parameterName);
        }
    }
}
