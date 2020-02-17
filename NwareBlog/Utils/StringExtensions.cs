using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace NwareBlog.Utils
{
    public static class StringExtensions
    {
        public static DateTime ToDateTime(this String value, String format, out bool succeeded)
        {
            DateTimeFormatInfo dtfi = DateTimeFormatInfo.InvariantInfo;
            var result = DateTime.TryParseExact(value, format, dtfi, DateTimeStyles.None, out DateTime newValue);
            succeeded = result;
            return newValue;
        }
    }
}
