using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AttributeRouting.Web.Mvc;
using BloomService.Domain.Entities;

namespace BloomService.Web.Areas.Apimobile.Controllers
{
    public class ImageController : ApiController
    {
        public string Post(string id, string file)
        {
            return "saved";
        }

    }
}
