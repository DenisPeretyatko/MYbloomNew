namespace BloomService.Web.Infrastructure.StorageProviders
{
    using System;

    public interface IStorageFolder
    {
        DateTime GetLastUpdated();

        string GetName();

        IStorageFolder GetParent();

        string GetPath();

        long GetSize();
    }
}