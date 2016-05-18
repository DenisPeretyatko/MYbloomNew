﻿namespace BloomService.Web.Services.Abstract
{
    using BloomService.Domain.Entities;
    using BloomService.Domain.Entities.Concrete;

    public interface ICustomerService : IEntityService<SageCustomer>
    {
    }
}