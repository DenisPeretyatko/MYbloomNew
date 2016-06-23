using System;
using System.Security.Claims;
using System.Threading;
using BloomService.Web.Infrastructure.Dependecy;
using BloomService.Web.Models;
using BloomService.Web.Services.Abstract;
using BloomService.Web.Utils;

namespace BloomService.Web.Controllers
{
    using System.Web.Mvc;

    [Authorize]
    public abstract class BaseController : Controller
    {
        private readonly Lazy<UserModel> _userModel = new Lazy<UserModel>(
            () => ComponentContainer.Current.Get<IAuthorizationService>().GetUser((ClaimsPrincipal)Thread.CurrentPrincipal));

        public UserModel UserModel
        {
            get { return _userModel.Value; }
        }       

        protected ActionResult Success()
        {
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        protected ActionResult Error(string message = "")
        {
            return Json(new { success = false, message }, JsonRequestBehavior.AllowGet);
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