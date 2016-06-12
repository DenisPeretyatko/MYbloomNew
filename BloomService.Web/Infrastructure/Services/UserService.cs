using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using BloomService.Web.Services.Abstract;
using System.Security.Claims;
using System.Threading;

namespace BloomService.Web.Services.Concrete
{
    public class UserService : IUserService
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Mail { get; set; }
        public string Type { get; set; }

        public UserService()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Login = identity.Claims.Where(c => c.Type == ClaimTypes.Name)
                               .Select(c => c.Value).SingleOrDefault();
            Password = identity.Claims.Where(c => c.Type == ClaimTypes.Surname)
                               .Select(c => c.Value).SingleOrDefault();
            Id = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                               .Select(c => c.Value).SingleOrDefault();
            Type = identity.Claims.Where(c => c.Type == ClaimTypes.Role)
                               .Select(c => c.Value).SingleOrDefault();
            Mail = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                               .Select(c => c.Value).SingleOrDefault();

            //var user = unitOfWork.Employees.Get().FirstOrDefault(x => x.Employee == Id);
            //if (user != null)
            //    Name = user.Name;
        }
    }
}