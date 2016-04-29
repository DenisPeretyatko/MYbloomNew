namespace BloomService.Web.Managers.Abstract
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Managers.Abstract.EntityManagers;

    public interface ICustomerApiManager : IEntityApiManager<SageCustomer>
    {
    }
}