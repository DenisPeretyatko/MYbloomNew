﻿namespace BloomService.Domain.Repositories.Abstract
{
    using BloomService.Domain.Entities;
    using BloomService.Domain.Entities.Concrete;

    public interface IEmployeeRepository : IRepository<SageEmployee>
    {
    }
}