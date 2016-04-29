namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using BloomService.Domain.Entities;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Services.Abstract;

    public class PartsController : ApiController
    {
        private readonly IPartService partSageApiService;

        public PartsController(IPartService partSageApiService)
        {
            this.partSageApiService = partSageApiService;
        }

        public IEnumerable<SagePart> Get()
        {
            return partSageApiService.Get();
        }

        public SagePart Get(string id)
        {
            return partSageApiService.Get(id);
        }
    }
}
