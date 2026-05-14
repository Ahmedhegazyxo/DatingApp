namespace Api.Helpers;
public static class FileExtensionMimeType
{
    public static readonly IReadOnlyDictionary<string, string> Map =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { ".jpg",  "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".png",  "image/png" },
            { ".gif",  "image/gif" },
            { ".webp", "image/webp" },

            { ".mp4",  "video/mp4" },
            { ".webm", "video/webm" },

            { ".pdf",  "application/pdf" },
            { ".txt",  "text/plain" },
            { ".json", "application/json" }
        };
}