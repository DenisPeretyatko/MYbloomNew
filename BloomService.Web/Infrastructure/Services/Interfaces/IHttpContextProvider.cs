using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace BloomService.Web.Infrastructure.Services.Interfaces
{
    public interface IHttpContextProvider
    {
        HttpContext Current { get; }
        HttpRequest GetHttpRequest();
        HttpResponse GetHttpResponse();
        IPrincipal GetUser();
    }
}