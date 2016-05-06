namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using BloomService.Domain.Entities;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Services.Abstract;

    public class PartsController : ApiController
    {
        private readonly IPartService partService;

        public PartsController(IPartService partService)
        {
            this.partService = partService;
        }

        public IEnumerable<SagePart> Get()
        {
            return partService.Get();
        }

        public SagePart Get(string id)
        {
            return partService.Get(id);
        }
    }
}
