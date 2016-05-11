namespace BloomService.Domain.Repositories.Concrete
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Repositories.Abstract;
    using MongoDB.Bson;
    using Services;
    using System.Linq;
    public class LocationRepository : EntityRepository<SageLocation>, ILocationRepository
    {
        public LocationRepository(string collectionName)
            : base(collectionName)
        {
        }

        public override bool Insert(SageLocation entity)
        {
            if (entity.Id == null)
            {
                entity.Id = ObjectId.GenerateNewId().ToString();
            }
            var parametersSearch = entity.Address + " " + entity.Address2 + " " + entity.City + " " + entity.ZIP + " " + entity.State;
            var location = GoogleApi.GetLocation(parametersSearch);
            if (location != null && location.result != null && location.result.Any())
            {
                var geometry = location.result.FirstOrDefault().geometry;
                if (geometry != null && geometry.location != null)
                {
                    entity.Latitude = geometry.location.lat;
                    entity.Longitude = geometry.location.lng;
                }
            }
            return collection.Insert(entity).HasLastErrorMessage;
        }
    }
}