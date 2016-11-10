using BloomService.Domain.Entities.Concrete;
using BloomService.Web.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace BloomService.Web.Infrastructure.SignalR.Implementation
{
    [HubName("bloomServiceHub")]
    public class BloomServiceHub : Hub, IBloomServiceHub
    {
        private readonly IHubConnectionContext<dynamic> _clients;

        public BloomServiceHub(IHubConnectionContext<dynamic> clients)
        {
            this._clients = clients;
        }

        public void UpdateWorkOrderPicture(SageImageWorkOrder model)
        {
            this._clients.All.UpdateWorkOrderPicture(model);
        }

        public void CreateAssignment(AssignmentHubModel model)
        {
            this._clients.All.createAssignment(model);
        }

        public void CreateWorkOrder(WorkorderViewModel model)
        {
            this._clients.All.createWorkorder(model);
        }

        public void DeleteAssigment(WorkorderViewModel model)
        {
            this._clients.All.deleteAssigment(model);
        }

        public void SendNotification(NotificationModel model)
        {
            this._clients.All.sendNotification(model);
        }

        public void UpdateTechnician(TechnicianModel model)
        {
            this._clients.All.updateTechnician(model);
        }

        public void UpdateTechnicianLocation(SageEmployee model)
        {
            this._clients.All.updateTechnicianLocation(model);
        }

        public void UpdateWorkOrder(WorkOrderModel model)
        {
            this._clients.All.UpdateWorkOrder(model);
        }

        public void UpdateSageWorkOrder(SageWorkOrder model)
        {
            this._clients.All.UpdateSageWorkOrder(model);
        }

        public void ShowAlert(SweetAlertModel model)
        {
            this._clients.All.ShowAlert(model);
        }

        public void AddNote(WorkOrderNoteModel model)
        {
            this._clients.All.AddNote(model);
        }
    }
}