using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BloomService.Web.Infrastructure
{
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