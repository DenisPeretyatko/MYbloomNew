using System;

namespace BloomService.Web.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static DateTime? TryAsDateTime(this string text)
        {
            if (text == null)
                throw new ArgumentException("text");

            DateTime result;
            return DateTime.TryParse(text, out result) ? (DateTime?)result : null;
        }

        public static string Safe(this string text, string defaultValue)
        {
            return text ?? defaultValue;
        }

        public static string Safe(this string text)
        {
            return text.Safe(string.Empty);
        }
    }
}