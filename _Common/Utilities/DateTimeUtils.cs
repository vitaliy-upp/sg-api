using System;
using System.Runtime.InteropServices;

namespace Utilities
{
    public class DateTimeUtils
    {
        private static bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        public static DateTime Now()
        {
            return DateTime.UtcNow;
        }

        public static DateTime NowByTimeZone(string timeZoneId, string tzDbName)
        {
            TimeZoneInfo tzInfo;
            if (IsWindows) 
            { 
                tzInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId); 
            }
            else 
            { 
                tzInfo = TimeZoneInfo.FindSystemTimeZoneById(tzDbName); 
            }

            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzInfo);
        }
    }
}
