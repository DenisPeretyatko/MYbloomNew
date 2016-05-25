namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Services.Abstract;

    public class LocationsController : ApiController
    {
        private readonly ILocationService locationService;

        public LocationsController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        public IEnumerable<SageLocation> Get()
        {
            return locationService.Get();
        }

        public SageLocation Get(string id)
        {
            return locationService.Get(id);
        }

        public IEnumerable<SageLocation> Post(Dictionary<string, string> properties)
        {
            return locationService.Add(properties);
        }

        public IEnumerable<SageLocation> Put(Dictionary<string, string> properties)
        {
            return locationService.Edit(properties);
        }
    }
}
