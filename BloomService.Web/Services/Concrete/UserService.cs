using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using BloomService.Web.Services.Abstract;

namespace BloomService.Web.Services.Concrete
{
    public class UserService : IUserService
    {
        public string GetId()
        {
            return "Rinta, Chriss";
        }
    }
}