using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using Api.Enums;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.AspNetCore.StaticFiles;

namespace Api.Entities;

public class ProfilePhoto : BaseEntity<Guid>
{
    public virtual Profile? Profile { get; private set; }
    public virtual Attachment? Attachment { get; private set; }
    public Guid AttachmentId { get; private set; }
    public static ProfilePhoto Create(string fileName, string rootPath, string fileExtension, AttachmentType attachmentType)
    {
        return new ProfilePhoto
        {
            Attachment = Attachment.Create(fileName, rootPath, fileExtension, attachmentType)
        };
    }
    public void Update(string fileName, string rootPath, string fileExtension, AttachmentType attachmentType)
    {
        if (this.Attachment != null)
            this.Attachment?.Update(rootPath, fileName, fileExtension, attachmentType);
    }
}