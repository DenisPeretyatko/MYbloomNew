namespace BloomService.Web.Infrastructure.StorageProviders
{
    using System.Collections.Generic;

    public interface IStorageProvider
    {
        IStorageFile CreateFile(string path);

        void CreateFolder(string path);

        void DeleteFile(string path);

        void DeleteFolder(string path);

        IStorageFile GetFile(string path);

        string GetPublicUrl(string path);

        bool IsFileExists(string path);

        bool IsFolderExits(string path);

        IEnumerable<IStorageFile> ListFiles(string path);

        IEnumerable<IStorageFolder> ListFolders(string path);

        void RenameFile(string path, string newPath);

        void RenameFolder(string path, string newPath);
    }
}