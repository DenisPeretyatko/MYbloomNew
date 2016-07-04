using Microsoft.AspNet.SignalR;
using System;
using BloomService.Web.Models;
using Microsoft.AspNet.SignalR.Hubs;
using BloomService.Domain.Entities.Concrete;

namespace BloomService.Web.Infrastructure.SignalR
{
    [HubName("bloomServiceHub")]
    public class BloomServiceHub : Hub, IBloomServiceHub
    {
        IHubConnectionContext<dynamic> _clients;

        public BloomServiceHub(IHubConnectionContext<dynamic> clients)
        {
            _clients = clients;
        }

        public void AddEquipment(EquipmentModel model)
        {
            throw new NotImplementedException();
        }

        public void AddWorkOrderPicture()
        {
            throw new NotImplementedException();
        }

        public void ChangeWorkOrderStatus(string id, string status)
        {
            throw new NotImplementedException();
        }

        public void CreateAssignment(MapViewModel model)
        {
            _clients.All.createAssignment(model);
        }

        public void CreateWorkOrder(SageWorkOrder model)
        {
            _clients.All.createWorkorder(model);
        }

        public void DeleteAssigment(AssignmentViewModel model)
        {
            _clients.All.deleteAssigment(model);
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void SendNotification(NotificationModel model)
        {
            _clients.All.sendNotification(model);
        }

        public void UpdateEquipment(EquipmentModel model)
        {
            throw new NotImplementedException();
        }

        public void UpdateTechnician(TechnicianModel model)
        {
            _clients.All.updateTechnician(model);
        }

        public void UpdateTechnicianLocation(SageEmployee model)
        {
            _clients.All.updateTechnicianLocation(model);
        }

        public void UpdateWorkOrder(WorkOrderModel model)
        {
            _clients.All.UpdateWorkOrder(model);
        }
    }
}