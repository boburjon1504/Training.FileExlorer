using Microsoft.Extensions.Options;
using Training.FileExplorer.Application.Common.Models.Filtering;
using Training.FileExplorer.Application.FileStorage.Brokers;
using Training.FileExplorer.Application.FileStorage.Models.Filtering;
using Training.FileExplorer.Application.FileStorage.Models.Settings;
using Training.FileExplorer.Application.FileStorage.Models.Storage;
using Training.FileExplorer.Application.FileStorage.Services;
using Training.FileExplorer.Infrastructure.FileStorage.Brokers;

namespace Training.FileExplorer.Infrastructure.FileStorage.Services;

public class FileService : IFileService
{
    private readonly FileFilterSettings _fileFilterSettings;
    private readonly FileStorageSettings _fileStorageSettings;
    private readonly IFileBroker _broker;

    public FileService(IOptions<FileStorageSettings> fileStorageSettings, IOptions<FileFilterSettings> fileFilterSettings, IFileBroker fileBroker)
    {
        _fileStorageSettings = fileStorageSettings.Value;
        _fileFilterSettings = fileFilterSettings.Value;
        _broker = fileBroker;
    }

    public ValueTask<StorageFile> GetFileByPathAsync(string filePath) =>
        !string.IsNullOrWhiteSpace(filePath)
            ? new ValueTask<StorageFile>(_broker.GetByPath(filePath))
            : throw new ArgumentNullException(nameof(filePath));
    

    public async ValueTask<IList<StorageFile>> GetFilesByPathAsync(IEnumerable<string> filesPath) =>
        await Task.Run(() =>
        {
            return filesPath.Select(filePath => _broker.GetByPath(filePath)).ToList();
        });


    public IEnumerable<StorageFilesSummary> GetFilesSummary(IEnumerable<StorageFile> files)
    {
        var filesType = files
            .Select(file => (File: file, Type: GetFileType(file.Path)));
        
        return filesType
            .GroupBy(file => file.Type)
            .Select(filesGroup => new StorageFilesSummary
            {
                FileType = filesGroup.Key,
                DisplayName = _fileFilterSettings.FileExtensions.FirstOrDefault(extension => extension.FileType == filesGroup.Key)?.DisplayName ??
                              "Other files",
                Count = filesGroup.Count(),
                Size = filesGroup.Sum(file => file.File.Size),
                ImageUrl = _fileFilterSettings.FileExtensions.FirstOrDefault(extension => extension.FileType == filesGroup.Key)?.ImageUrl ??
                           _fileStorageSettings.FileImageUrl
            });
    }

    

    public StorageFileType GetFileType(string filePath)
    {
        var fileExtension = Path.GetExtension(filePath).TrimStart('.');
        var matchedFileType = _fileFilterSettings.FileExtensions.FirstOrDefault(extension => extension.Extensions.Contains(fileExtension));
        
        return matchedFileType?.FileType ?? StorageFileType.Other;

    }
}
