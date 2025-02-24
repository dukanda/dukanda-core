using DukandaCore.Application.Common.Constants;

namespace DukandaCore.Application.Common.Models;



public class Error
{
    public Error(string messageCode)
    {
        if (ErrorCodes.Errors.TryGetValue(messageCode, out var errorDetails))
        {
            MessageCode = errorDetails.Code;
            Message = errorDetails.Message;
        }
        else
        {
            MessageCode = "UNKNOWN_ERROR";
            Message = "An unknown error occurred.";
        }
    }

    public string MessageCode { get; }
    public string Message { get; }

    public static Error None => new(string.Empty);

    public static implicit operator Error(string messageCode) => new(messageCode);

    public static implicit operator string(Error error) => error.Message;
}
