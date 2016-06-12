namespace Sage.WebApi.Infratructure.Service
{
    using BloomService.Domain.Entities.Concrete;
    using System.Collections.Generic;

    public interface IServiceOdbc
{
        List<Dictionary<string, object>> Customers();

        List<Dictionary<string, object>> Trucks();

        void UnassignWorkOrder(string id);

        SageWorkOrder EditWorkOrder(SageWorkOrder workOrder);

        List<SageWorkOrder> WorkOrders();
    }
}
