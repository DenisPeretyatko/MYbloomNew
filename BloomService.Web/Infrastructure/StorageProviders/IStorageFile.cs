namespace BloomService.Web.Infrastructure.StorageProviders
{
    using System;
    using System.IO;

    public interface IStorageFile
    {
        string GetFileType();

        string GetFullPath();

        DateTime GetLastUpdated();

        string GetName();

        string GetPath();

        long GetSize();

        Stream OpenRead();

        Stream OpenWrite();
    }
}