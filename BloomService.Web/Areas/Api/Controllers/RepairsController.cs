namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using BloomService.Domain.Entities;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Services.Abstract;

    public class RepairsController : ApiController
    {
        private readonly IRepairService repairSageApiService;

        public RepairsController(IRepairService repairSageApiService)
        {
            this.repairSageApiService = repairSageApiService;
        }

        public IEnumerable<SageRepair> Get()
        {
            return repairSageApiService.Get();
        }

        public SageRepair Get(string id)
        {
            return repairSageApiService.Get(id);
        }
    }
}
