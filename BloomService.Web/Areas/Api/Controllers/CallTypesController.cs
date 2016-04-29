namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using BloomService.Domain.Entities;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Services.Abstract;

    public class CallTypesController : ApiController
    {
        private readonly ICallTypeService callTypeSageApiService;

        public CallTypesController(ICallTypeService callTypeSageApiService)
        {
            this.callTypeSageApiService = callTypeSageApiService;
        }

        public IEnumerable<SageCallType> Get()
        {
            return callTypeSageApiService.Get();
        }

        public SageCallType Get(string id)
        {
            return callTypeSageApiService.Get(id);
        }
    }
}
