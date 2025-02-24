namespace DukandaCore.Application.Common.Interfaces;

public interface ICloudinaryService
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName);
    Task<bool> DeleteFileAsync(string publicId);
}
