namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using BloomService.Domain.Entities;
    using BloomService.Web.Services.Abstract;

    public class LocationsController : ApiController
    {
        private readonly ILocationSageApiService locationSageApiService;

        public LocationsController(ILocationSageApiService locationSageApiService)
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

        public IEnumerable<SageLocation> Delete(PropertyDictionary properties)
        {
            return locationSageApiService.Delete(properties);
        }
    }
}
