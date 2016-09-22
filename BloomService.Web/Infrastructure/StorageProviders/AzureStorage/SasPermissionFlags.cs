using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloomService.Web.Infrastructure.StorageProviders.AzureStorage
{
    [Flags]
    public enum SasPermissionFlags
    {
        None = 0,
        Read = 1,
        Write = 2,
        Delete = 4,
        List = 8,
    }
}