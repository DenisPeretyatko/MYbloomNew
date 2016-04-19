using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using System.Web.Script.Serialization;
using AttributeRouting.Web.Mvc;
using BloomService.Domain.Entities;
using BloomService.Web.Infrastructure;

namespace BloomService.Web.Areas.Apimobile.Controllers
{
    public class WorkOrderController : ApiController
    {
        public IEnumerable<SageWorkOrder> Get(string token)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"public\mock\getWorkorders.json");
            var sr = new StreamReader(path);
            var json = sr.ReadToEnd();
            var list = new JavaScriptSerializer().Deserialize<IEnumerable<SageWorkOrder>>(json);
            return list;
        }
        public SageWorkOrder Get(string id, string token)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"public\mock\getWorkorder.json");
            var sr = new StreamReader(path);
            var json = sr.ReadToEnd();
            var workorder = new JavaScriptSerializer().Deserialize<SageWorkOrder>(json);
            return workorder;
        }
        public bool Post(string id, string token)
        {
            return true;
        }
    }
}
