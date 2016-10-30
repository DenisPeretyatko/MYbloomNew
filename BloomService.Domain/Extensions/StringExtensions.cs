using System;
using System.Web;

namespace BloomService.Domain.Extensions
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

        public static double AsDouble(this string text)
        {
            return double.Parse(text);
        }

        public static decimal AsDecimal(this string text)
        {
            return decimal.Parse(text);
        }

        public static double? AsDoubleSafe(this string text)
        {
            double result;
            return double.TryParse(text, out result) ? result : (double?)null;
        }

        public static double AsDoubleSafe(this string text, double defaultValue)
        {
            double result;
            return double.TryParse(text, out result) ? result : defaultValue;
        }

        public static int AsInt(this string text)
        {
            return int.Parse(text);
        }

        public static int? AsIntSafe(this string text)
        {
            int result;
            return int.TryParse(text, out result) ? result : (int?)null;
        }

        public static int AsIntSafe(this string text, int defaultValue)
        {
            int result;
            return int.TryParse(text, out result) ? result : defaultValue;
        }

        public static long AsLong(this string text)
        {
            return long.Parse(text);
        }

        public static long? AsLongSafe(this string text)
        {
            long result;
            return long.TryParse(text, out result) ? result : (long?)null;
        }

        public static long AsLongSafe(this string text, long defaultValue)
        {
            long result;
            return long.TryParse(text, out result) ? result : defaultValue;
        }

        public static string Sanitize(this string text)
        {
            return text.Replace("'", "&apos;")
                       .Replace("\"", "&quot;")
                       .Replace("&", "&amp;")
                       .Replace("<", "&lt;")
                       .Replace(">", "&gt;");
        }

        public static string UriCombine(this string val, string append)
        {
            if (String.IsNullOrEmpty(val)) return append;
            if (String.IsNullOrEmpty(append)) return val;
            return val.TrimEnd('/') + "/" + append.TrimStart('/');
        }

        public static string DecodeSafeHtmlString(this string comment)
        {
            while (comment.Contains("&amp") || comment.Contains("&quot") || comment.Contains("&lt") || comment.Contains("&gt") || comment.Contains("&apos") || comment.Contains("&nbsp"))
                comment = HttpUtility.HtmlDecode(comment);
            return comment;
        }
    }
}