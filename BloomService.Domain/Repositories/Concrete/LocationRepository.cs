namespace BloomService.Domain.Repositories.Concrete
{
    using System.Linq;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Repositories.Abstract;
    using BloomService.Domain.Services;

    using MongoDB.Bson;
    using UnitOfWork;
    using System.Collections.Generic;
    public class LocationRepository : EntityRepository<SageLocation>, ILocationRepository
    {
        private List<SageWorkOrder> workOrders;

        public LocationRepository(string collectionName)
            : base(collectionName)
        {
        }

        public override bool Add(SageLocation entity)
        {
            if (entity.Id == null)
            {
                entity.Id = ObjectId.GenerateNewId().ToString();
            }
            if(workOrders == null)
            {
                var workOrderRepository = new MongoDbUnitOfWork().GetEntities<SageWorkOrder>();
                workOrders = workOrderRepository.Get().Where(x => x.Status == "Open").ToList();
            }
            if (workOrders.Any(x => x.Location == entity.Name))
            {
                System.Threading.Thread.Sleep(1000);
                var parametersSearch = entity.Address + " " + entity.City + " " + entity.ZIP + " " + entity.State;
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
            }
            return Collection.Insert(entity).HasLastErrorMessage;
        }
        public override IQueryable<SageLocation> Get()
        {
            return base.Get();
        }
    }
}