using System;
using System.Web.Http;
using AttributeRouting.Web.Mvc;

namespace BloomService.Web.Areas.Apimobile.Controllers
{
    public class LocationController : ApiController
    {
        public bool Post(int technicianId, decimal lat, decimal lng, string time)
        {
            return true;
        }
    }
}
