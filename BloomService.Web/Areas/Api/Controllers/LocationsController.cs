namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using BloomService.Domain.Entities;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Services.Abstract;

    public class LocationsController : ApiController
    {
        private readonly ILocationService locationSageApiService;

        public LocationsController(ILocationService locationSageApiService)
        {
            this.locationSageApiService = locationSageApiService;
        }

        public IEnumerable<SageLocation> Get()
        {
            return locationSageApiService.Get();
        }

        public SageLocation Get(string id)
        {
            return locationSageApiService.Get(id);
        }

        public IEnumerable<SageLocation> Post(PropertyDictionary properties)
        {
            return locationSageApiService.Add(properties);
        }

        public IEnumerable<SageLocation> Put(PropertyDictionary properties)
        {
            return locationSageApiService.Edit(properties);
        }
    }
}
