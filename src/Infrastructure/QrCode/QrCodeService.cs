using DukandaCore.Application.Common.Interfaces;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace DukandaCore.Infrastructure.QrCode;

public class QrCodeService : IQrCodeService
{
    public async Task<string> GenerateQrCodeAsBase64(string content)
    {
        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new PngByteQRCode(qrCodeData);
        var imageBytes = qrCode.GetGraphic(20);
        
        return await Task.FromResult($"data:image/png;base64,{Convert.ToBase64String(imageBytes)}");
    }
} 