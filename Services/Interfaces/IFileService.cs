namespace OnlineShop.Services.Interfaces;

public interface IFileService
{
    Task<string> UploadAsync(
        IFormFile file,
        string folderName);

    void Delete(string filePath);

    bool IsImage(IFormFile file);

    bool IsValidSize(
        IFormFile file,
        int maxSizeInMb);
}