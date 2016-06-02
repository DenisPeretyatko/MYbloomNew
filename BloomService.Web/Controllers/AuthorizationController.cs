using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BloomService.Domain.Entities;
using BloomService.Web.Infrastructure;
using AttributeRouting.Web.Mvc;
using BloomService.Web.Infrastructure.Constants;
using BloomService.Web.Models;
using BloomService.Web.Services.Abstract;

namespace BloomService.Web.Controllers
{
    public class AuthorizationController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [GET("Authorization/Login/{name}/{password}")]
        public ActionResult Login(string name, string password)
        {
            if (name == "user" && password == "111")
                return Json("success", JsonRequestBehavior.AllowGet);
            return Json("failed", JsonRequestBehavior.AllowGet);
        }
    }
}