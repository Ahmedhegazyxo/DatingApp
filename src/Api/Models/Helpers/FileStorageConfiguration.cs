using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Api.Helpers;
public class FileStorageConfiguration
{
    public string RootFolderPath {get;set;} = default!;
    public string RequestRootFolderPath {get;set;} = default!;
}