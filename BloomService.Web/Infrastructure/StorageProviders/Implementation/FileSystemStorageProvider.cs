namespace BloomService.Web.Infrastructure.StorageProviders.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class FileSystemStorageProvider : IStorageProvider
    {
        private readonly string baseUrl;

        private readonly string storagePath;

        public FileSystemStorageProvider(string basePath, string baseUrl)
        {
            storagePath = basePath;
            if (!baseUrl.EndsWith("/"))
            {
                baseUrl = baseUrl + '/';
            }

            this.baseUrl = baseUrl;
        }

        public IStorageFile CreateFile(string path)
        {
            if (File.Exists(Map(path)))
            {
                throw new InvalidOperationException("File " + path + " already exists");
            }

            var fileInfo = new FileInfo(Map(path));
            File.WriteAllBytes(Map(path), new byte[0]);

            return new FileSystemStorageFile(Fix(path), fileInfo);
        }

        public void CreateFolder(string path)
        {
            if (Directory.Exists(Map(path)))
            {
                throw new InvalidOperationException("Directory " + path + " already exists");
            }

            Directory.CreateDirectory(Map(path));
        }

        public void DeleteFile(string path)
        {
            if (!File.Exists(Map(path)))
            {
                throw new InvalidOperationException("File " + path + " does not exist");
            }

            File.Delete(Map(path));
        }

        public void DeleteFolder(string path)
        {
            if (!Directory.Exists(Map(path)))
            {
                throw new InvalidOperationException("Directory " + path + " does not exist");
            }

            Directory.Delete(Map(path), true);
        }

        public IStorageFile GetFile(string path)
        {
            if (!File.Exists(Map(path)))
            {
                throw new InvalidOperationException("File " + path + " does not exist");
            }

            return new FileSystemStorageFile(Fix(path), new FileInfo(Map(path)));
        }

        public string GetPublicUrl(string path)
        {
            return baseUrl + path.Replace(Path.DirectorySeparatorChar, '/');
        }

        public bool IsFileExists(string path)
        {
            return File.Exists(Map(path));
        }

        public bool IsFolderExits(string path)
        {
            return Directory.Exists(Map(path));
        }

        public IEnumerable<IStorageFile> ListFiles(string path)
        {
            if (!Directory.Exists(Map(path)))
            {
                throw new InvalidOperationException("Directory " + path + " does not exist");
            }

            return
                new DirectoryInfo(Map(path)).GetFiles()
                    .Where(fi => !IsHidden(fi))
                    .Select<FileInfo, IStorageFile>(
                        fi => new FileSystemStorageFile(Path.Combine(Fix(path), fi.Name), fi))
                    .ToList();
        }

        public IEnumerable<IStorageFolder> ListFolders(string path)
        {
            if (Directory.Exists(Map(path)))
            {
                return
                    new DirectoryInfo(Map(path)).GetDirectories()
                        .Where(di => !IsHidden(di))
                        .Select<DirectoryInfo, IStorageFolder>(
                            di => new FileSystemStorageFolder(Path.Combine(Fix(path), di.Name), di))
                        .ToList();
            }
            try
            {
                Directory.CreateDirectory(Map(path));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"The folder could not be created at path: {path}. {ex}");
            }

            return
                new DirectoryInfo(Map(path)).GetDirectories()
                    .Where(di => !IsHidden(di))
                    .Select<DirectoryInfo, IStorageFolder>(
                        di => new FileSystemStorageFolder(Path.Combine(Fix(path), di.Name), di))
                    .ToList();
        }

        public void RenameFile(string path, string newPath)
        {
            if (!File.Exists(Map(path)))
            {
                throw new InvalidOperationException("File " + path + " does not exist");
            }

            if (File.Exists(Map(newPath)))
            {
                throw new InvalidOperationException("File " + newPath + " already exists");
            }

            File.Move(Map(path), Map(newPath));
        }

        public void RenameFolder(string path, string newPath)
        {
            if (!Directory.Exists(Map(path)))
            {
                throw new InvalidOperationException("Directory " + path + "does not exist");
            }

            if (Directory.Exists(Map(newPath)))
            {
                throw new InvalidOperationException("Directory " + newPath + " already exists");
            }

            Directory.Move(Map(path), Map(newPath));
        }

        internal static bool IsHidden(FileSystemInfo di)
        {
            return (di.Attributes & FileAttributes.Hidden) != 0;
        }

        private static string Fix(string path)
        {
            return string.IsNullOrEmpty(path)
                       ? string.Empty
                       : Path.DirectorySeparatorChar != '/' ? path.Replace('/', Path.DirectorySeparatorChar) : path;
        }

        private string Map(string path)
        {
            return string.IsNullOrEmpty(path) ? storagePath : Path.Combine(storagePath, path);
        }
    }
}