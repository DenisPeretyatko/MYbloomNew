using BloomService.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloomService.Web.Infrastructure.SignalR
{
    public interface IBloomServiceHub
    {
        void Disconnect();
        void SendNotification(string message);
        void CreateAssignment(AssignmentModel model);
        void DeleteAssigment(int id);
        void CreateWorkOrder(WorkOrderModel model);
        void UpdateWorkOrder(WorkOrderModel model);
        void UpdateTechnician(TechnicianModel model);
        void AddEquipment(EquipmentModel model);
        void UpdateEquipment(EquipmentModel model);
        void UpdateTechnicianLocation(string technicianId);
        void ChangeWorkOrderStatus(string id, string status);
        void AddWorkOrderPicture();
    }
}
