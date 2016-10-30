using System;

namespace BloomService.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        //public static string ToSageDate(this DateTime date )
        //{
        //    return date.ToUniversalTime().ToString("yyyy-MM-dd");
        //}

        //public static string ToSageTime(this DateTime date)
        //{
        //    return date.ToUniversalTime().TimeOfDay.ToString();
        //}

        //public static DateTime NowIfMin(this DateTime date)
        //{
        //    if (date == DateTime.MinValue)
        //    {
        //        date = DateTime.Now;
        //    }
        //    return date;
        //}

        public static DateTime GetLocalDate(this DateTime date, string timezoneId)
        {
            var usEastern = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            var utc = date.ToUniversalTime();
            var result = utc.AddHours(usEastern.BaseUtcOffset.Hours);
            return result;
        }
    }
}