namespace BloomService.Domain.Repositories.Concrete
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Repositories.Abstract;

    public class LocationRepository : EntityRepository<SageLocation>, ILocationRepository
    {
        public LocationRepository(string collectionName)
            : base(collectionName)
        {
        }
    }
}