using BloomService.Domain.Entities;
using BloomService.Web.Services.Abstract;
using System.Collections.Generic;
using System.Web.Http;

namespace BloomService.Web.Areas.Api
{
    public class PartsController : ApiController
    {
        private readonly IPartSageApiService partSageApiService;

        public PartsController(IPartSageApiService partSageApiService)
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

        public IEnumerable<SagePart> Post(Properties properties)
        {
            return partSageApiService.Add(properties);
        }

        public IEnumerable<SagePart> Put(Properties properties)
        {
            return partSageApiService.Edit(properties);
        }

        public IEnumerable<SagePart> Delete(Properties properties)
        {
            return partSageApiService.Edit(properties);
        }
    }
}
