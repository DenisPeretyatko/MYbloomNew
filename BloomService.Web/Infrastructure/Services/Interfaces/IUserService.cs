namespace BloomService.Web.Infrastructure.Services.Interfaces
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