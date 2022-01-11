using System;

namespace Codebug.PaypalTokenGenerator
{
    public static class StringExtensions
    {
        public static void EmptyOrThrowError(this string str, string nameOf)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException(nameOf);
            }
        }
    }
}
