using OnlineShop.Services.Interfaces;

namespace OnlineShop.Services.Implementations;

public class FileService : IFileService
{
    private static readonly string[] AllowedExtensions =
    [
        ".jpg",
        ".jpeg",
        ".png",
        ".webp"
    ];
    private readonly IWebHostEnvironment _environment;

    public FileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> UploadAsync(
        IFormFile file,
        string folderName)
    {
        var uploadsFolder = Path.Combine(
            _environment.WebRootPath,
            "uploads",
            folderName);

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var fileName =
            $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        var filePath =
            Path.Combine(uploadsFolder, fileName);

        using var stream =
            new FileStream(filePath, FileMode.Create);

        await file.CopyToAsync(stream);

        return $"/uploads/{folderName}/{fileName}";
    }

    public void Delete(string filePath)
    {
        var fullPath = Path.Combine(
            _environment.WebRootPath,
            filePath.TrimStart('/'));

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }

    public bool IsImage(IFormFile file)
    {
        var extension =
            Path.GetExtension(file.FileName)
                .ToLowerInvariant();

        return AllowedExtensions.Contains(extension);
    }

    public bool IsValidSize(IFormFile file, int maxSizeInMb)
    {
        return file.Length <=
            maxSizeInMb * 1024 * 1024;
    }
}