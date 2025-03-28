public interface IQrCodeService
{
    Task<string> GenerateQrCodeAsBase64(string content);
} 