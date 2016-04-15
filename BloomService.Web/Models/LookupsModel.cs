using System.Collections.Generic;

namespace BloomService.Web.Models
{
    public class LookupsModel
    {
        public List<LocationModel> Locations { get; set; }
        public List<CustomerModel> Customers { get; set; }
        public List<CallTypeModel> Calltypes { get; set; }
        public List<ProblemModel> Problems { get; set; }
        public List<string> Ratesheets { get; set; }
        public List<EmployeeModel> Employes { get; set; }
        public List<EquipmentModel> Equipment { get; set; }
        public List<string> Hours { get; set; }
        public List<string> PaymentMethods { get; set; }
    }

}