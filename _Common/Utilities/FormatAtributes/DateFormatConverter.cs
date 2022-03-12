using System;
using Newtonsoft.Json.Converters;

namespace Utilities.FormatAtributes
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
