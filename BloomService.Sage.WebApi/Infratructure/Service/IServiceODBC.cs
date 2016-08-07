namespace Sage.WebApi.Infratructure.Service
{
    using BloomService.Domain.Entities.Concrete;
    using System.Collections.Generic;

    public interface IServiceOdbc
{
        List<SageCustomer> Customers();

        //List<Dictionary<string, object>> Trucks();

        void UnassignWorkOrder(string id);

        void EditWorkOrder(SageWorkOrder workOrder);

        //List<SageWorkOrder> WorkOrders();

        List<SageRateSheet> RateSheets();

        List<SagePermissionCode> PermissionCodes();

        void DeleteWorkOrderItems(int workOrderId, IEnumerable<int> ids);

        void EditWorkOrderStatus(string id, string status);

        void EditWorkJcJob(string id, string jcjob);
    }
}
