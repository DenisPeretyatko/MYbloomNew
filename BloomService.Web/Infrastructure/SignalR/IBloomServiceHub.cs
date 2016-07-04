using BloomService.Domain.Entities.Concrete;
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
        void SendNotification(NotificationModel model);
        void CreateAssignment(MapViewModel model);
        void DeleteAssigment(AssignmentViewModel id);
        void CreateWorkOrder(SageWorkOrder model);
        void UpdateWorkOrder(WorkOrderModel model);
        void UpdateTechnician(TechnicianModel model);
        void AddEquipment(EquipmentModel model);
        void UpdateEquipment(EquipmentModel model);
        void UpdateTechnicianLocation(SageEmployee model);
        void ChangeWorkOrderStatus(string id, string status);
        void AddWorkOrderPicture();
    }
}
