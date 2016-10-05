using System;
using System.Configuration;
using BloomService.Domain.Extensions;
using MongoDB.Driver;
using RestSharp.Extensions.MonoHttp;

namespace BloomService.Web.Infrastructure.Dependecy
{
    public static class ComponentContainer
    {

        public static IComponentContainer Current { get; set; }

        public static DateTime GetLocalDate(this DateTime date)
        {
            var _settings = BloomServiceConfiguration.FromWebConfig(ConfigurationManager.AppSettings);
            var timezonId = _settings.CurrentTimezone;
            var USEastern = TimeZoneInfo.FindSystemTimeZoneById(timezonId);
            var utc = date.ToUniversalTime();
            var result = utc.AddHours(USEastern.BaseUtcOffset.Hours);
            //var result = TimeZoneInfo.ConvertTime(date.ToUniversalTime(), USEastern);
            return result;
        }

        public static string DecodeSafeHtmlString(this string comment)
        {
            while (comment.Contains("&amp") || comment.Contains("&quot") || comment.Contains("&lt") || comment.Contains("&gt") || comment.Contains("&apos") || comment.Contains("&nbsp"))
                comment = HttpUtility.HtmlDecode(comment);
            return comment;
        }
         
    }
}