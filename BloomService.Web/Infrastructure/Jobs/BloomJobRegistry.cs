using BloomService.Domain.Entities.Concrete;
using BloomService.Web.Infrastructure.Dependecy;
using BloomService.Web.Infrastructure.Services.Interfaces;

using Common.Logging;
using FluentScheduler;
using System.Collections.Generic;
using System.Linq;

namespace BloomService.Web.Infrastructure.Jobs
{
    using Mongo;
    using System.Threading.Tasks;
    using Domain.Entities.Concrete.Auxiliary;
    using System;
    public partial class BloomJobRegistry : Registry
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(BloomJobRegistry));
        private readonly ISageApiProxy _proxy;
        private readonly BloomServiceConfiguration _settings;
        private readonly IRepository _repository;
        private readonly object _iosPushNotificationLock = new object();
        private readonly object _syncSageLock = new object();
        private readonly object _checkTechniciansLock = new object();
        private readonly IHttpContextProvider _httpContextProvider;
        private readonly ILocationService _locationService;
        private readonly object _keepAlive = new object();
        private readonly INotificationService _notification;
        private List<LateTechnician> lateTechnicians;

        public IQueryable<SageEmployee> technicians { get; set; }
        public SageResponse<SageRateSheet> rateSheetArray { get; set; }
        public IQueryable<SageEmployee> techniciansAvailable { get; set; }
        public SageResponse<SagePermissionCode> permissionCodesArray { get; set; }
        public SageResponse<SageWorkOrder> workOrders { get; set; }
        public IEnumerable<SageEmployee> emploees { get; set; }
        public SageResponse<SageCallType> callTypeArray { get; set; }
        public SageResponse<SageEquipment> equipments { get; set; }
        public SageResponse<SageAssignment> assignments { get; set; }
        public SageResponse<SageProblem> problems { get; set; }
        public SageResponse<SageRepair> repairs { get; set; }
        public SageResponse<SageLocation> locations { get; set; }
        public SageResponse<SageCustomer> customers { get; set; }
        public SageResponse<SagePart> parts { get; set; }

        public class LateTechnician
        {
            public long Employee;
            public bool TenMinutes;
            public bool ThirtyMinutes;
            public long WorkOrder;
        }


        public BloomJobRegistry()
        {
            _proxy = ComponentContainer.Current.Get<ISageApiProxy>();
            _settings = ComponentContainer.Current.Get<BloomServiceConfiguration>();
            _repository = ComponentContainer.Current.Get<IRepository>();
            _httpContextProvider = ComponentContainer.Current.Get<IHttpContextProvider>();
            _locationService = ComponentContainer.Current.Get<ILocationService>();
            _notification = ComponentContainer.Current.Get<INotificationService>();
            lateTechnicians = new List<LateTechnician>();
           
            SendNotifications();
            SendRequest();
            Synchronization();
            CheckTechnicians();
        }

        public void GetEntities()
        {
            rateSheetArray = _proxy.GetRateSheets();
            permissionCodesArray = _proxy.GetPermissionCodes();
            workOrders = _proxy.GetWorkorders();
            emploees = _proxy.GetEmployees().Entities.Where(x => !string.IsNullOrEmpty(x.JCJob));
            callTypeArray = _proxy.GetCalltypes();
            equipments = _proxy.GetEquipment();
            assignments = _proxy.GetAssignments();
            problems = _proxy.GetProblems();
            repairs = _proxy.GetRepairs();
            locations = _proxy.GetLocations();
            customers = _proxy.GetCustomers();
            parts = _proxy.GetParts();
        }
    }
}