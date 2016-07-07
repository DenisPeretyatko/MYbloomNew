namespace BloomService.Web.Infrastructure.Services.Interfaces
{
    using BloomService.Domain.Entities.Concrete;

    public interface ILocationService
    {
        void ResolveLocation(SageLocation entity);
    }
}