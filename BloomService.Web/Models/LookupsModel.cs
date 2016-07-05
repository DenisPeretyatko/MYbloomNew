using System.Collections.Generic;
using BloomService.Domain.Entities.Concrete;

namespace BloomService.Web.Models
{
    public class LookupsModel
    {
        public IEnumerable<LocationModel> Locations { get; set; }
        public IEnumerable<CustomerModel> Customers { get; set; }
        public IEnumerable<CallTypeModel> Calltypes { get; set; }
        public IEnumerable<ProblemModel> Problems { get; set; }
        public IEnumerable<EmployeeModel> Employes { get; set; }
        public IEnumerable<EquipmentModel> Equipment { get; set; }
        public IEnumerable<RepairModel> Hours { get; set; }
        public IEnumerable<NotificationModel> Notifications { get; set; }
        public string NotificationTime { get; set; }

        public IEnumerable<RateSheetModel> RateSheets { get; set; }
        public IEnumerable<PermissionCodeModel> PermissionCodes { get; set; }
        public IEnumerable<PaymentMethodModel> PaymentMethods { get; set; }
        public IEnumerable<PartModel> Parts { get; set; }
    }
}