namespace BloomService.Web.Infrastructure.SignalR.Implementation
{
    using System;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Models;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    [HubName("bloomServiceHub")]
    public class BloomServiceHub : Hub, IBloomServiceHub
    {
        IHubConnectionContext<dynamic> _clients;

        public BloomServiceHub(IHubConnectionContext<dynamic> clients)
        {
            this._clients = clients;
        }

        public void AddEquipment(EquipmentModel model)
        {
            throw new NotImplementedException();
        }

        public void UpdateWorkOrderPicture(SageImageWorkOrder model)
        {
            this._clients.All.UpdateWorkOrderPicture(model);
        }

        public void ChangeWorkOrderStatus(string id, string status)
        {
            throw new NotImplementedException();
        }

        public void CreateAssignment(MapViewModel model)
        {
            this._clients.All.createAssignment(model);
        }

        public void CreateWorkOrder(SageWorkOrder model)
        {
            this._clients.All.createWorkorder(model);
        }

        public void DeleteAssigment(AssignmentViewModel model)
        {
            this._clients.All.deleteAssigment(model);
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void SendNotification(NotificationModel model)
        {
            this._clients.All.sendNotification(model);
        }

        public void UpdateEquipment(EquipmentModel model)
        {
            throw new NotImplementedException();
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
    }
}