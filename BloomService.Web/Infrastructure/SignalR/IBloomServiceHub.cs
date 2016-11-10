using BloomService.Domain.Entities.Concrete;
using BloomService.Web.Models;

namespace BloomService.Web.Infrastructure.SignalR
{
    public interface IBloomServiceHub
    {
        void SendNotification(NotificationModel model);
        void CreateAssignment(AssignmentHubModel model);
        void DeleteAssigment(WorkorderViewModel id);
        void CreateWorkOrder(WorkorderViewModel model);
        void UpdateWorkOrder(WorkOrderModel model);
        void UpdateTechnician(TechnicianModel model);
        void UpdateTechnicianLocation(SageEmployee model);
        void UpdateWorkOrderPicture(SageImageWorkOrder model);
        void UpdateSageWorkOrder(SageWorkOrder model);
        void ShowAlert(SweetAlertModel model);
        void AddNote(WorkOrderNoteModel model);
    }
}
