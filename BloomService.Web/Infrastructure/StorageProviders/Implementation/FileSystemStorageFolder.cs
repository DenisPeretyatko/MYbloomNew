namespace BloomService.Web.Infrastructure.StorageProviders.Implementation
{
    using System;
    using System.IO;
    using System.Linq;

    public class FileSystemStorageFolder : IStorageFolder
    {
        private readonly DirectoryInfo directoryInfo;

        private readonly string path;

        public FileSystemStorageFolder(string path, DirectoryInfo directoryInfo)
        {
            this.path = path;
            this.directoryInfo = directoryInfo;
        }

        public DateTime GetLastUpdated()
        {
            return directoryInfo.LastWriteTime;
        }

        public string GetName()
        {
            return directoryInfo.Name;
        }

        public IStorageFolder GetParent()
        {
            if (directoryInfo.Parent != null)
            {
                return new FileSystemStorageFolder(Path.GetDirectoryName(path), directoryInfo.Parent);
            }

            throw new InvalidOperationException(
                "Directory " + directoryInfo.Name + " does not have a parent directory");
        }

        public string GetPath()
        {
            return path;
        }

        public long GetSize()
        {
            return GetDirectorySize(directoryInfo);
        }

        private static long GetDirectorySize(DirectoryInfo directoryInfo)
        {
            var fileInfos = directoryInfo.GetFiles();
            var size =
                fileInfos.Where(fileInfo => !FileSystemStorageProvider.IsHidden(fileInfo))
                    .Sum(fileInfo => fileInfo.Length);
            var directoryInfos = directoryInfo.GetDirectories();
            size +=
                directoryInfos.Where(dInfo => !FileSystemStorageProvider.IsHidden(dInfo))
                    .Sum(dInfo => GetDirectorySize(dInfo));
            return size;
        }
    }
}