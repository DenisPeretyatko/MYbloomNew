using System;
using System.Security.Claims;
using System.Threading;
using BloomService.Web.Infrastructure.Dependecy;
using BloomService.Web.Models;
using System.Web.Mvc;
using BloomService.Web.Infrastructure.Services.Interfaces;
using BloomService.Web.Infrastructure.Utils;

namespace BloomService.Web.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        private readonly Lazy<UserModel> _userModel = new Lazy<UserModel>(() =>
                ComponentContainer.Current.Get<IAuthorizationService>()
                .GetUser((ClaimsPrincipal)Thread.CurrentPrincipal));

        public UserModel UserModel => _userModel.Value;

        protected ActionResult Success()
        {
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        protected ActionResult Success(object data)
        {
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        protected ActionResult Error(string message = "", string innerError = "")
        {
            return Json(new { success = false, message, innerError }, JsonRequestBehavior.AllowGet);
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = int.MaxValue
            };
        }
    }
}