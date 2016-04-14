﻿using BloomService.Domain.Entities;
using BloomService.Web.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace BloomService.Web.Areas.Api
{
    public class WorkOrdersController : ApiController
    {
        private readonly IWorkOrderSageApiService workOrderSageApiService;

        public WorkOrdersController(IWorkOrderSageApiService workOrderSageApiService)
        {
            this.workOrderSageApiService = workOrderSageApiService;
        }

        public IEnumerable<SageWorkOrder> Get()
        {
            return workOrderSageApiService.Get();
        }

        public SageWorkOrder Get(string id)
        {
            return workOrderSageApiService.Get(id);
        }

        public IEnumerable<SageWorkOrder> Post(Properties properties)
        {
            return workOrderSageApiService.Add(properties);
        }

        public IEnumerable<SageWorkOrder> Put(Properties properties)
        {
            return workOrderSageApiService.Edit(properties);
        }

        public IEnumerable<SageWorkOrder> Delete(Properties properties)
        {
            return workOrderSageApiService.Delete(properties);
        }
    }
}
