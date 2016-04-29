namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using BloomService.Domain.Entities;
    using BloomService.Web.Services.Abstract;

    public class CallTypesController : ApiController
    {
        private readonly ICallTypeSageApiService callTypeSageApiService;

        public CallTypesController(ICallTypeSageApiService callTypeSageApiService)
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

        public IEnumerable<SageCallType> Post(PropertyDictionary properties)
        {
            return callTypeSageApiService.Add(properties);
        }

        public IEnumerable<SageCallType> Put(PropertyDictionary properties)
        {
            return callTypeSageApiService.Edit(properties);
        }

        public IEnumerable<SageCallType> Delete(PropertyDictionary properties)
        {
            return callTypeSageApiService.Edit(properties);
        }
    }
}
