namespace BloomService.Web.Infrastructure.Constants
{
    using Domain.Entities.Concrete;
    using Models;
    using System.Collections.Generic;

    public static class WorkOrderStatus
    {
        public static List<SageWorkOrderStatus> Status = new List<SageWorkOrderStatus> {
            new SageWorkOrderStatus() { Status = "Open", Value= 0 },
            new SageWorkOrderStatus() { Status = "Return Required", Value= 1 },
            new SageWorkOrderStatus() { Status = "Work Complete", Value = 2 },
            new SageWorkOrderStatus() { Status = "Closed", Value = 3 },
        };
    }
}