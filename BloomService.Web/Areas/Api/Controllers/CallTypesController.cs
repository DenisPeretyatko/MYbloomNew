using BloomService.Domain.Entities;
using BloomService.Web.Services.Abstract;
using System.Collections.Generic;
using System.Web.Http;

namespace BloomService.Web.Areas.Api
{
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

        public IEnumerable<SageCallType> Post(Properties properties)
        {
            return callTypeSageApiService.Add(properties);
        }

        public IEnumerable<SageCallType> Put(Properties properties)
        {
            return callTypeSageApiService.Edit(properties);
        }

        public IEnumerable<SageCallType> Delete(Properties properties)
        {
            return callTypeSageApiService.Edit(properties);
        }
    }
}
