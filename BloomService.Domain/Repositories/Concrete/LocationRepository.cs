namespace BloomService.Domain.Repositories.Concrete
{
    using System.Linq;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Repositories.Abstract;
    using BloomService.Domain.Services;

    using MongoDB.Bson;

    public class LocationRepository : EntityRepository<SageLocation>, ILocationRepository
    {
        public LocationRepository(string collectionName)
            : base(collectionName)
        {
        }

        public bool Insert(SageLocation entity)
        {
            if (entity.Id == null)
            {
                entity.Id = ObjectId.GenerateNewId().ToString();
            }

            var parametersSearch = entity.Address + " " + entity.Address2 + " " + entity.City + " " + entity.ZIP + " "
                                   + entity.State;
            var location = GoogleApi.GetLocation(parametersSearch);
            System.Threading.Thread.Sleep(1000);
            if (location != null && location.result != null && location.result.Any())
            {
                var geometry = location.result.FirstOrDefault().geometry;
                if (geometry != null && geometry.location != null)
                {
                    entity.Latitude = geometry.location.lat;
                    entity.Longitude = geometry.location.lng;
                }
            }

            return Collection.Insert(entity).HasLastErrorMessage;
        }
    }
}