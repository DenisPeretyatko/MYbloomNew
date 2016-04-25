using BloomService.Web.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloomService.Web.Services.Concrete
{
    public class UserService : IUserService
    {
        public string GetId()
        {
            return "11";
        }
    }
}