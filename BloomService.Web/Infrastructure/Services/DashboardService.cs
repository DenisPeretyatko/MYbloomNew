using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BloomService.Domain.Entities.Concrete;
using BloomService.Web.Infrastructure.Constants;
using BloomService.Web.Infrastructure.Mongo;
using BloomService.Web.Infrastructure.Services.Interfaces;
using BloomService.Web.Models;

namespace BloomService.Web.Infrastructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IRepository _repository;

        public DashboardService(IRepository repository)
        {
            _repository = repository;
        }

        public LookupsModel GetLookups()
        {
            var lookups = new LookupsModel();
            var locations = this._repository.GetAll<SageLocation>().ToList();
            var calltypes = this._repository.GetAll<SageCallType>().ToList();
            var problems = this._repository.GetAll<SageProblem>().ToList();
            var employes = this._repository.GetAll<SageEmployee>().ToList();
            var equipment = this._repository.GetAll<SageEquipment>().ToList();
            var customer = this._repository.GetAll<SageCustomer>().ToList();
            var repairs = this._repository.GetAll<SageRepair>().ToList();
            var ratesheets = this._repository.GetAll<SageRateSheet>().Where(x => x.QINACTIVE == "N").ToList();
            var permissionCodes = this._repository.GetAll<SagePermissionCode>().ToList();
            var parts = this._repository.GetAll<SagePart>().Where(x => x.PartNumber.StartsWith("R-") && x.Inactive == "No").ToList();

            lookups.Locations = Mapper.Map<List<SageLocation>, List<LocationModel>>(locations);
            lookups.Calltypes = Mapper.Map<List<SageCallType>, List<CallTypeModel>>(calltypes);
            lookups.Problems = Mapper.Map<List<SageProblem>, List<ProblemModel>>(problems);
            lookups.Employes = Mapper.Map<List<SageEmployee>, List<EmployeeModel>>(employes);
            lookups.Equipment = Mapper.Map<List<SageEquipment>, List<EquipmentModel>>(equipment);
            lookups.Customers = Mapper.Map<List<SageCustomer>, List<CustomerModel>>(customer);
            lookups.Hours = Mapper.Map<List<SageRepair>, List<RepairModel>>(repairs);

            lookups.RateSheets = Mapper.Map<List<SageRateSheet>, List<RateSheetModel>>(ratesheets);
            lookups.PermissionCodes = Mapper.Map<List<SagePermissionCode>, List<PermissionCodeModel>>(permissionCodes);

            lookups.PaymentMethods = PaymentMethod.PaymentMethods;
            lookups.Parts = Mapper.Map<List<SagePart>, List<PartModel>>(parts);
            lookups.Status = WorkOrderStatus.StatusForManager;

            return lookups;
        }
    }
}