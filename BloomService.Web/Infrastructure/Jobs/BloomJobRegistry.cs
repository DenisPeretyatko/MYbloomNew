using BloomService.Domain.Entities.Concrete;
using BloomService.Web.Infrastructure.Dependecy;
using BloomService.Web.Infrastructure.Services.Interfaces;
using Common.Logging;
using FluentScheduler;
using System.Collections.Generic;
using System.Linq;
using BloomService.Web.Infrastructure.SignalR;
using BloomService.Web.Infrastructure.Mongo;
using BloomService.Domain.Entities.Concrete.Auxiliary;

namespace BloomService.Web.Infrastructure.Jobs
{

    public partial class BloomJobRegistry : Registry
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(BloomJobRegistry));
        private readonly ISageApiProxy _proxy;
        private readonly IRepository _repository;
        private readonly BloomServiceConfiguration _settings;

        private readonly object _iosPushNotificationLock = new object();
        private readonly object _syncSageLock = new object();
        private readonly object _checkTechniciansLock = new object();
        private readonly object _keepAlive = new object();

        private readonly IHttpContextProvider _httpContextProvider;
        private readonly ILocationService _locationService;
        private readonly INotificationService _notification;
        private readonly IBloomServiceHub _hub;

        private readonly List<LateTechnician> _lateTechnicians;
        private List<SageLocationCache> _locationsCache;

        public SageEmployee[] Technicians { get; set; }
        public SageResponse<SageRateSheet> RateSheetArray { get; set; }
        public SageResponse<SagePermissionCode> PermissionCodesArray { get; set; }
        public SageResponse<SageWorkOrder> WorkOrders { get; set; }
        public SageResponse<SageCallType> CallTypeArray { get; set; }
        public SageResponse<SageEquipment> Equipments { get; set; }
        public SageResponse<SageAssignment> Assignments { get; set; }
        public SageResponse<SageProblem> Problems { get; set; }
        public SageResponse<SageRepair> Repairs { get; set; }
        public SageResponse<SageLocation> Locations { get; set; }
        public SageResponse<SageCustomer> Customers { get; set; }
        public SageResponse<SagePart> Parts { get; set; }
        public SageResponse<SageNote> WorkOrderNotes { get; set; }
        public SageResponse<SageWorkOrderItem> WorkOrderItems { get; set; }

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
            _lateTechnicians = new List<LateTechnician>();
            _hub = ComponentContainer.Current.Get<IBloomServiceHub>();
            SendNotifications();
            SendRequest();
            Synchronization();
            CheckTechnicians();
        }

        public void GetEntities()
        {
            RateSheetArray = _proxy.GetRateSheets();
            PermissionCodesArray = _proxy.GetPermissionCodes();
            WorkOrders = _proxy.GetWorkorders();
            Technicians = _proxy.GetEmployees().Entities.Where(x => !string.IsNullOrEmpty(x.JCJob)).ToArray();
            CallTypeArray = _proxy.GetCalltypes();
            Equipments = _proxy.GetEquipment();
            Assignments = _proxy.GetAssignments();
            Problems = _proxy.GetProblems();
            Repairs = _proxy.GetRepairs();
            Locations = _proxy.GetLocations();
            Customers = _proxy.GetCustomers();
            Parts = _proxy.GetParts();
            WorkOrderNotes = _proxy.GetNotes();
        }
    }
}