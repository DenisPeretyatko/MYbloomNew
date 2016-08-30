using System;
using System.IO;

namespace BloomService.Web.Infrastructure.StorageProviders
{
    public interface IStorageFile
    {
        string GetPath();
        string GetFullPath();
        string GetName();
        long GetSize();
        DateTime GetLastUpdated();
        string GetFileType();

        Stream OpenRead();
        Stream OpenWrite();
    }
}