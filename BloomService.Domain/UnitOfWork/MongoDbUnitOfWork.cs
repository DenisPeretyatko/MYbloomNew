namespace BloomService.Domain.UnitOfWork
{
    using System.Linq;

    using BloomService.Domain.Attributes;
    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Repositories.Abstract;
    using BloomService.Domain.Repositories.Concrete;

    public class MongoDbUnitOfWork : IUnitOfWork
    {
        private IAssignmentRepository assignmentRepository;

        private ICallTypeRepository callTypeRepository;

        private IChangeRepository changeRepository;

        private ICustomerRepository customerRepository;

        private IDepartmentRepository departmentRepository;

        private IEmployeeRepository employeeRepository;

        private IEquipmentRepository equipmentRepository;

        private ILocationRepository locationRepository;

        private IPartRepository partRepository;

        private IProblemRepository problemRepository;

        private IRepairRepository repairRepository;

        private IWorkOrderRepository workOrderRepository;

        public IAssignmentRepository Assignments
        {
            get
            {
                return assignmentRepository
                       ?? (assignmentRepository = new AssignmentRepository(GetCollectionName<SageAssignment>()));
            }
        }

        public ICallTypeRepository CallTypes
        {
            get
            {
                return callTypeRepository
                       ?? (callTypeRepository = new CallTypeRepository(GetCollectionName<SageCallType>()));
            }
        }

        public IChangeRepository Changes
        {
            get
            {
                return changeRepository
                       ?? (changeRepository = new ChangeRepository(GetCollectionName<SageChange>()));
            }
        }

        public ICustomerRepository Customers
        {
            get
            {
                return customerRepository
                       ?? (customerRepository = new CustomerRepository(GetCollectionName<SageCustomer>()));
            }
        }

        public IDepartmentRepository Departments
        {
            get
            {
                return departmentRepository
                       ?? (departmentRepository = new DepartmentRepository(GetCollectionName<SageDepartment>()));
            }
        }

        public IEmployeeRepository Employees
        {
            get
            {
                return employeeRepository
                       ?? (employeeRepository = new EmployeeRepository(GetCollectionName<SageEmployee>()));
            }
        }

        public IEquipmentRepository Equipment
        {
            get
            {
                return equipmentRepository
                       ?? (equipmentRepository = new EquipmentRepository(GetCollectionName<SageEquipment>()));
            }
        }

        public ILocationRepository Locations
        {
            get
            {
                return locationRepository
                       ?? (locationRepository = new LocationRepository(GetCollectionName<SageLocation>()));
            }
        }

        public IPartRepository Parts
        {
            get
            {
                return partRepository ?? (partRepository = new PartRepository(GetCollectionName<SagePart>()));
            }
        }

        public IProblemRepository Problems
        {
            get
            {
                return problemRepository
                       ?? (problemRepository = new ProblemRepository(GetCollectionName<SageProblem>()));
            }
        }

        public IRepairRepository Repairs
        {
            get
            {
                return repairRepository ?? (repairRepository = new RepairRepository(GetCollectionName<SageRepair>()));
            }
        }

        public IWorkOrderRepository WorkOrders
        {
            get
            {
                return workOrderRepository
                       ?? (workOrderRepository = new WorkOrderRepository(GetCollectionName<SageWorkOrder>()));
            }
        }

        public string GetCollectionName<TEntity>() where TEntity : class, IEntity
        {
            var collectionNameAttribute =
                typeof(TEntity).GetCustomAttributes(typeof(CollectionNameAttribute), true).FirstOrDefault() as
                CollectionNameAttribute;

            if (collectionNameAttribute != null)
            {
                return collectionNameAttribute.Name;
            }

            return null;
        }

        public IRepository<TEntity> GetEntities<TEntity>() where TEntity : class, IEntity
        {
            var entityRepository = new EntityRepository<TEntity>(GetCollectionName<TEntity>());
            return entityRepository;
        }
    }
}