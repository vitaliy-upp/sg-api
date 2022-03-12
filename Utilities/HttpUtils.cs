using System;

namespace Utilities
{
    public static class HttpUtils
    {
        public static string GetEventIdFromRequestPath(string uri)
        {
            var splitted  = uri
                .Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (splitted[1].ToLower() == "event-space")
            {
                return splitted[2];
            }

            return "";
        }
    }
}
