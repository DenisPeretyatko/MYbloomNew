namespace Sage.WebApi.Infratructure.Service
{
    using BloomService.Domain.Entities.Concrete;
    using System.Collections.Generic;

    public interface IServiceOdbc
{
        List<SageCustomer> Customers();

        void UnassignWorkOrder(string id);

        void EditWorkOrder(SageWorkOrder workOrder);

        List<SageRateSheet> RateSheets();

        List<SagePermissionCode> PermissionCodes();

        void DeleteWorkOrderItems(int workOrderId, IEnumerable<int> ids);

        void EditWorkOrderStatus(string id, string status);

        void EditWorkJcJob(string id, string jcjob);

        void AddNote(SageNote note);

        void EditNote(SageNote note);

        void DeleteNote(string id);

        List<SageNote> GetNotes(string id);
    }
}
