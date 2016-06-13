namespace BloomService.Web.Services.Abstract
{
    using BloomService.Domain.Entities.Concrete;

    public interface ILocationService
    {
        void ResolveLocation(SageLocation entity);
    }
}