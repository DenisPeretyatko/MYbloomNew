namespace BloomService.Web.Infrastructure.Hubs
{
    using System;
    using System.IO;
    using System.Web.Script.Serialization;

    public static class JsonHelper
    {
        public static object GetObjects(string filename)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"public\mock\" + filename);
            var sr = new StreamReader(path);
            var json = sr.ReadToEnd();
            return new JavaScriptSerializer().Deserialize<object>(json);
        }
    }
}