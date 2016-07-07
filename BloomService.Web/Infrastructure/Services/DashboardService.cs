namespace BloomService.Web.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Infrastructure.Constants;
    using BloomService.Web.Infrastructure.Mongo;
    using BloomService.Web.Infrastructure.Services.Interfaces;
    using BloomService.Web.Models;

    public class DashboardService : IDashboardService
    {
        private readonly IRepository _repository;

        public DashboardService(IRepository repository)
        {
            this._repository = repository;
        }

        public LookupsModel GetLookups()
        {
            var lookups = new LookupsModel();
            var locations = this._repository.GetAll<SageLocation>();
            var calltypes = this._repository.GetAll<SageCallType>();
            var problems = this._repository.GetAll<SageProblem>();
            var employes = this._repository.GetAll<SageEmployee>();
            var equipment = this._repository.GetAll<SageEquipment>();
            var customer = this._repository.GetAll<SageCustomer>();
            var repairs = this._repository.GetAll<SageRepair>();
            var ratesheets = this._repository.GetAll<SageRateSheet>().Where(x => x.QINACTIVE == "N");
            var permissionCodes = this._repository.GetAll<SagePermissionCode>();
            var parts = this._repository.GetAll<SagePart>();

            lookups.Locations = Mapper.Map<List<SageLocation>, List<LocationModel>>(locations.ToList());
            lookups.Calltypes = Mapper.Map<List<SageCallType>, List<CallTypeModel>>(calltypes.ToList());
            lookups.Problems = Mapper.Map<List<SageProblem>, List<ProblemModel>>(problems.ToList());
            lookups.Employes = Mapper.Map<List<SageEmployee>, List<EmployeeModel>>(employes.ToList());
            lookups.Equipment = Mapper.Map<List<SageEquipment>, List<EquipmentModel>>(equipment.ToList());
            lookups.Customers = Mapper.Map<List<SageCustomer>, List<CustomerModel>>(customer.ToList());
            lookups.Hours = Mapper.Map<List<SageRepair>, List<RepairModel>>(repairs.ToList());

            lookups.RateSheets = Mapper.Map<List<SageRateSheet>, List<RateSheetModel>>(ratesheets.ToList());
            lookups.PermissionCodes = Mapper.Map<List<SagePermissionCode>, List<PermissionCodeModel>>(permissionCodes.ToList());

            lookups.PaymentMethods = PaymentMethod.PaymentMethods;
            lookups.Parts = Mapper.Map<List<SagePart>, List<PartModel>>(parts.ToList());

            return lookups;
        }
    }
}