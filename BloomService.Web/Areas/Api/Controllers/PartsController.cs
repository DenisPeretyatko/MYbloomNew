namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using BloomService.Domain.Entities;
    using BloomService.Web.Services.Abstract;

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

        public IEnumerable<SagePart> Post(PropertyDictionary properties)
        {
            return partSageApiService.Add(properties);
        }

        public IEnumerable<SagePart> Put(PropertyDictionary properties)
        {
            return partSageApiService.Edit(properties);
        }

        public IEnumerable<SagePart> Delete(PropertyDictionary properties)
        {
            return partSageApiService.Edit(properties);
        }
    }
}
