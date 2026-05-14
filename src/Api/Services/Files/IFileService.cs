using Api.Helpers;
namespace Api.Services;

public interface IFileService
{
    public Task<bool> CreateFile(FileMetaData fileMetaData);
    public FileData? RetreiveFile(FileMetaData fileMetaData);
}