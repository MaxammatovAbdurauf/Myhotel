using Microsoft.AspNetCore.Http;

namespace Myhotel.Services.IServices;

public interface IFileService
{
    ValueTask<List<string>> SaveFileAsync(List<IFormFile> files, string FileFolder, string FileType);
}