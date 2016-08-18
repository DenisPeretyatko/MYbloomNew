namespace BloomService.Web.Infrastructure.Services.Interfaces
{
    using BloomService.Domain.Entities.Concrete;

    public interface ILocationService
    {
        void ResolveLocation(SageLocation entity);
        double Distance(decimal latitude1, decimal longitude1, decimal latitude2, decimal longitude2);
    }
}