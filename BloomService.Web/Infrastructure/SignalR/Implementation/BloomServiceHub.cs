using Microsoft.AspNet.SignalR;
using System;
using BloomService.Web.Models;

namespace BloomService.Web.Infrastructure.SignalR
{
    public class BloomServiceHub : Hub, IBloomServiceHub
    {
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

        public void CreateAssignment(AssignmentModel model)
        {
            throw new NotImplementedException();
        }

        public void CreateWorkOrder(WorkOrderModel model)
        {
            throw new NotImplementedException();
        }

        public void DeleteAssigment(int id)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void SendNotification(string message)
        {
            throw new NotImplementedException();
        }

        public void UpdateEquipment(EquipmentModel model)
        {
            throw new NotImplementedException();
        }

        public void UpdateTechnician(TechnicianModel model)
        {
            throw new NotImplementedException();
        }

        public void UpdateTechnicianLocation(string technicianId)
        {
            throw new NotImplementedException();
        }

        public void UpdateWorkOrder(WorkOrderModel model)
        {
            throw new NotImplementedException();
        }
    }
}