using System.IO.Compression;
using Api.Helpers;
using Api.Repositories;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediaController : ControllerBase
{
    private readonly IFileService _fileService;
    private readonly IAttachmentRepository _attachmentRepository;
    public MediaController(IFileService fileService,
     IAttachmentRepository attachmentRepository)
    {
        _fileService = fileService;
        _attachmentRepository = attachmentRepository;
    }
    [AllowAnonymous]
    [HttpGet("download/{attachmentId}")]
    public async Task<IActionResult> Download(Guid attachmentId, CancellationToken cancellationToken = default!)
    {
        Attachment? attachment = await _attachmentRepository.ReadyByIdAsyncAsNoTracking(attachmentId, cancellationToken);
        FileData? fileData = _fileService.RetreiveFile(new FileMetaData
        {
            FileName = attachment!.FileName,
            FileExtension = attachment.FileExtension.StartsWith(".")? attachment.FileExtension : "." + attachment.FileExtension,
            FileRootPath = attachment.RootPath
        });
        if (fileData != null)
            return PhysicalFile(fileData.FileFullPath, fileData.ContentType, enableRangeProcessing: true);
        else
            return NotFound("File Not Found");
    }
}