﻿namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using BloomService.Domain.Entities;
    using BloomService.Web.Services.Abstract;

    public class RepairsController : ApiController
    {
        private readonly IRepairSageApiService repairSageApiService;

        public RepairsController(IRepairSageApiService repairSageApiService)
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

        public IEnumerable<SageRepair> Post(Properties properties)
        {
            return repairSageApiService.Add(properties);
        }

        public IEnumerable<SageRepair> Put(Properties properties)
        {
            return repairSageApiService.Edit(properties);
        }

        public IEnumerable<SageRepair> Delete(Properties properties)
        {
            return repairSageApiService.Edit(properties);
        }
    }
}