﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Lust.Infra.Files.Storage
{
    public interface IAzureBlobStorage
    {
        Task UploadAsync(string blobName, string filePath);
        Task UploadAsync(string blobName, Stream stream);
        Task<MemoryStream> DownloadAsync(string blobName);
        Task DownloadAsync(string blobName, string path);
        Task<MemoryStream> GetStreamAsync(string blobName);
        Task DeleteAsync(string blobName);
        Task<bool> ExistsAsync(string blobName);
        Task<List<AzureBlobItem>> ListAsync();
        Task<List<AzureBlobItem>> ListAsync(string rootFolder);
        Task<List<string>> ListFoldersAsync();
        Task<List<string>> ListFoldersAsync(string rootFolder);
        Uri StorageUri();
    }
}
