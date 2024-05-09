using Microsoft.AspNetCore.Http;

namespace Craft.Application.Common.Helpers;

public class UploadHelper
{
    public static async Task<FileInfo> UploadFile(IFormFile file, string fileName = null, string directoryPath = null, bool useOriginalName = false)
    {
        if (file == null || file.Length == 0)
        {
            return null;
        }

        if (string.IsNullOrEmpty(fileName))
        {
            fileName = useOriginalName ? file.FileName : Guid.NewGuid().ToString();
        }

        if (!Path.HasExtension(fileName))
        {
            var extension = Path.GetExtension(file.FileName);
            fileName += extension;
        }

        if (string.IsNullOrEmpty(directoryPath))
        {
            directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");
        }

        Directory.CreateDirectory(directoryPath);

        var filePath = Path.Combine(directoryPath, fileName);

        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return new FileInfo(filePath);
    }
}
