namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using BloomService.Domain.Entities;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Services.Abstract;

    public class RepairsController : ApiController
    {
        private readonly IRepairService repairService;

        public RepairsController(IRepairService repairService)
        {
            this.repairService = repairService;
        }

        public IEnumerable<SageRepair> Get()
        {
            return repairService.Get();
        }

        public SageRepair Get(string id)
        {
            return repairService.Get(id);
        }
    }
}
