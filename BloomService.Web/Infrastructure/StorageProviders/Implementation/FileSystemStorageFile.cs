namespace BloomService.Web.Infrastructure.StorageProviders.Implementation
{
    using System;
    using System.IO;

    public class FileSystemStorageFile : IStorageFile
    {
        private readonly FileInfo fileInfo;
        private readonly string path;

        public FileSystemStorageFile(string path, FileInfo fileInfo)
        {
            this.path = path;
            this.fileInfo = fileInfo;
        }

        #region Implementation of IStorageFile
        public string GetFullPath()
        {
            return Path.Combine(path, fileInfo.Name);
        }

        public string GetPath()
        {
            return path;
        }

        public string GetName()
        {
            return fileInfo.Name;
        }

        public long GetSize()
        {
            return fileInfo.Length;
        }

        public DateTime GetLastUpdated()
        {
            return fileInfo.LastWriteTime;
        }

        public string GetFileType()
        {
            return fileInfo.Extension;
        }

        public Stream OpenRead()
        {
            return new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);
        }

        public Stream OpenWrite()
        {
            return new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.ReadWrite);
        }
        #endregion
    }
}
