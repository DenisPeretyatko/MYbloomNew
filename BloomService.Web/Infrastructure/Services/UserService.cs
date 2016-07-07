namespace BloomService.Web.Infrastructure.Services
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;

    using BloomService.Web.Infrastructure.Services.Interfaces;

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

            this.Login = identity.Claims.Where(c => c.Type == ClaimTypes.Name)
                               .Select(c => c.Value).SingleOrDefault();
            this.Password = identity.Claims.Where(c => c.Type == ClaimTypes.Surname)
                               .Select(c => c.Value).SingleOrDefault();
            this.Id = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                               .Select(c => c.Value).SingleOrDefault();
            this.Type = identity.Claims.Where(c => c.Type == ClaimTypes.Role)
                               .Select(c => c.Value).SingleOrDefault();
            this.Mail = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                               .Select(c => c.Value).SingleOrDefault();

            //var user = unitOfWork.Employees.Get().FirstOrDefault(x => x.Employee == Id);
            //if (user != null)
            //    Name = user.Name;
        }
    }
}