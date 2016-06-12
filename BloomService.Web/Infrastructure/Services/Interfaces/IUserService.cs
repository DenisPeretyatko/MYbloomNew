using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloomService.Web.Services.Abstract
{
    public interface IUserService
    {
        string Login { get; set; }
        string Password { get; set; }
        string Name { get; set; }
        string Id { get; set; }
        string Mail { get; set; }
        string Type { get; set; }
    }
}