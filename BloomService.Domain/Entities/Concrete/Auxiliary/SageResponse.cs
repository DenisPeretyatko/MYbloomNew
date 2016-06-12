﻿namespace BloomService.Domain.Entities.Concrete.Auxiliary
{
    using System.Collections.Generic;

    public class SageResponse<TEntity>
    {
        public List<TEntity> Entities { get; set; }

        public TEntity Entity { get; set; }

        public string ErrorMassage { get; set; }

        public bool IsSucceed { get; set; }

        public List<string> Strings { get; set; }
    }
}