using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using System.Web.Script.Serialization;
using BloomService.Domain.Entities;

namespace BloomService.Web.Areas.Apimobile.Controllers
{
    using BloomService.Domain.Entities.Concrete;

    public class WorkOrderController : ApiController
    {
        public IEnumerable<SageWorkOrder> Get()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"public\mock\getWorkorders.json");
            var sr = new StreamReader(path);
            var json = sr.ReadToEnd();
            var list = new JavaScriptSerializer().Deserialize<IEnumerable<SageWorkOrder>>(json);
            return list;
        }
        public SageWorkOrder Get(string id)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"public\mock\getWorkorder.json");
            var sr = new StreamReader(path);
            var json = sr.ReadToEnd();
            var workorder = new JavaScriptSerializer().Deserialize<SageWorkOrder>(json);
            return workorder;
        }

        public bool Post(string id)
        {
            return true;
        }
    }
}
