using Api.Enums;

namespace Api.Entities;

public class Attachment : BaseEntity<Guid>
{
    public string FileName { get; private set; } = default!;
    public AttachmentType AttachmentType { get; private set; }
    public string FileExtension { get; private set; } = default!;
    public string RootPath { get; private set; } = default!;
    public static Attachment Create(string fileName, string rootPath, string fileExtension, AttachmentType attachmentType)
    {
        return new Attachment
        {
            FileName = fileName,
            FileExtension = fileExtension,
            AttachmentType = attachmentType,
            RootPath = rootPath
        };
    }
    public void Update(string rootPath, string fileName, string fileExtension, AttachmentType attachmentType)
    {
        RootPath = rootPath;
        FileName = fileName;
        FileExtension = fileExtension;
        AttachmentType = attachmentType;
    }
}