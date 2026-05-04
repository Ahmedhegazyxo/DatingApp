using System.IO.Compression;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediaController : ControllerBase
{
    [HttpPost("Upload")]
    [AllowAnonymous]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        string fileName= Path.ChangeExtension(file.FileName, ".gz");
        await using FileStream writeStream = System.IO.File.Create(fileName);
        await using Stream readStream = file.OpenReadStream();
        await using GZipStream gzipStream = new GZipStream(writeStream, CompressionMode.Compress);
        await readStream.CopyToAsync(gzipStream);
        return Ok(new {FilePath = fileName, Accepted = true});
    }
}