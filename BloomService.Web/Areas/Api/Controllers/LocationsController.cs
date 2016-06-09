namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
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

        public IHttpActionResult Get()
        {
            var result = locationService.Get();
            if (result.Any())
            {
                return Ok(result);
            }

            return NotFound();
        }

        public IHttpActionResult Get(string id)
        {
            var result = locationService.Get(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        public IHttpActionResult Post(SageLocation location)
        {
            var result = locationService.Add(location);
            if (result.IsSucceed)
            {
                return Ok(result.Entity);
            }

            return NotFound();
        }

        public IHttpActionResult Put(SageLocation location)
        {
            var result = locationService.Edit(location);
            if (result.IsSucceed)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
