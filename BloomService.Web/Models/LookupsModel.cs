﻿using System.Collections.Generic;

namespace BloomService.Web.Models
{
    public class LookupsModel
    {
        public IEnumerable<LocationModel> Locations { get; set; }
        public IEnumerable<CustomerModel> Customers { get; set; }
        public IEnumerable<CallTypeModel> Calltypes { get; set; }
        public IEnumerable<ProblemModel> Problems { get; set; }
        public IEnumerable<string> Ratesheets { get; set; }
        public IEnumerable<EmployeeModel> Employes { get; set; }
        public IEnumerable<EquipmentModel> Equipment { get; set; }
        public IEnumerable<string> Hours { get; set; }
        public IEnumerable<string> PaymentMethods { get; set; }
    }

}