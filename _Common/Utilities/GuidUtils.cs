using System;

namespace Utilities
{
    public static class GuidUtils
    {
        public static string GetTokenKey()
        {
            return Guid.NewGuid().ToString()
                .Replace("-", "");
        }
    }
}
