using Api.Helpers;
using Microsoft.Extensions.Options;

namespace Api.Services;

public class FileService : IFileService
{
    private readonly FileStorageConfiguration _fileStorageConfigurations;
    public FileService(IOptions<FileStorageConfiguration> options)
    {
        _fileStorageConfigurations = options.Value;
    }
   public async Task<bool> CreateFile(FileMetaData fileMetaData)
{
    string folderPath = Path.Combine(
        _fileStorageConfigurations.RootFolderPath,
        fileMetaData.FileRootPath);

    Directory.CreateDirectory(folderPath);

    string filePath = Path.Combine(
        folderPath,
        fileMetaData.FileName + fileMetaData.FileExtension);

    using var fileStream = new FileStream(
        filePath,
        FileMode.Create,
        FileAccess.Write,
        FileShare.None,
        bufferSize: 81920,
        useAsync: true);

    using var stream = fileMetaData.OpenStream!();
    await stream.CopyToAsync(fileStream);
    return true;
}

    public FileData? RetreiveFile(FileMetaData fileMetaData)
    {
        bool fileExists = File.Exists(Path.Combine(_fileStorageConfigurations.RootFolderPath,
         fileMetaData.FileRootPath,
          fileMetaData.FileName + fileMetaData.FileExtension));
        if (!fileExists)
            return null;
        else
        {
            return new FileData
            {
                FileFullPath = Path.Combine(_fileStorageConfigurations.RootFolderPath,
                fileMetaData.FileRootPath, fileMetaData.FileName.ToString() + fileMetaData.FileExtension),
                ContentType = FileExtensionMimeType.Map[fileMetaData.FileExtension]
            };
        }
    }
}