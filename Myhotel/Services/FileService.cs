using Microsoft.AspNetCore.Http;
using Myhotel.Services.IServices;

namespace Myhotel.Services;

public class FileService : IFileService
{
    public async ValueTask<List<string>> SaveFileAsync (List<IFormFile> files, string FileFolder, string FileType)
    {
        var mainPath = Path.Combine("wwwroot", FileFolder, FileType);
        if (!Directory.Exists(mainPath))
            Directory.CreateDirectory(mainPath);

        var filePaths = new List<string>();

        foreach(var file in files)
        {
            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);

            var memory = new MemoryStream();
            await file.CopyToAsync(memory);
            await File.WriteAllBytesAsync(Path.Combine(mainPath,fileName), memory.ToArray());

            filePaths.Add(fileName);
        }

        return filePaths;
    }
}