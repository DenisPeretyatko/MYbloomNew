namespace BloomService.Domain.Repositories.Abstract
{
    using BloomService.Domain.Entities.Concrete;

    public interface ILocationRepository : IRepository<SageLocation>
    {
        bool Delete(SageLocation entity);

        bool Update(SageLocation entity);
    }
}