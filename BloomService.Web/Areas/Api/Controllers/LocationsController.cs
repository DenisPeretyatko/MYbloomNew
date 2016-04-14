using BloomService.Domain.Entities;
using BloomService.Web.Services.Abstract;
using System.Collections.Generic;
using System.Web.Http;

namespace BloomService.Web.Areas.Api
{
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

        public IEnumerable<SageLocation> Post(Properties properties)
        {
            return locationSageApiService.Add(properties);
        }

        public IEnumerable<SageLocation> Put(Properties properties)
        {
            return locationSageApiService.Edit(properties);
        }

        public IEnumerable<SageLocation> Delete(Properties properties)
        {
            return locationSageApiService.Delete(properties);
        }
    }
}
