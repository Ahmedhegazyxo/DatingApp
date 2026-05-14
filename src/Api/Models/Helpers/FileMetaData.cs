namespace Api.Helpers;
public class FileMetaData
{
    public string FileName {get;set;}
    public string FileRootPath {get;set;} = string.Empty;
    public string FileExtension {get;set;} = string.Empty;
    public Func<Stream>? OpenStream {get;set;}
}