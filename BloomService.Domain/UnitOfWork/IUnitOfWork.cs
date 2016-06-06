namespace BloomService.Domain.UnitOfWork
{
    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Repositories.Abstract;

    public interface IUnitOfWork
    {
        IAssignmentRepository Assignments { get; }

        ICallTypeRepository CallTypes { get; }

        ICustomerRepository Customers { get; }

        IDepartmentRepository Departments { get; }

        IEmployeeRepository Employees { get; }

        IEquipmentRepository Equipment { get; }

        ILocationRepository Locations { get; }

        IPartRepository Parts { get; }

        IProblemRepository Problems { get; }

        IRepairRepository Repairs { get; }

        IWorkOrderRepository WorkOrders { get; }

        IChangeRepository Changes { get; }

        ITechnicianLocationRepository TechnicianLocation { get; }

        IRepository<TEntity> GetEntities<TEntity>() where TEntity : class, IEntity;
    }
}