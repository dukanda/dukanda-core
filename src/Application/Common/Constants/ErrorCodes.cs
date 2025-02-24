namespace DukandaCore.Application.Common.Constants;


public static class ErrorCodes
{
    public const string EmailAlreadyInUse = "EMAIL_ALREADY_IN_USE";
    public const string InvalidCredentials = "INVALID_CREDENTIALS";
    public const string UserNotFound = "USER_NOT_FOUND";
    public const string InvalidOrExpiredToken = "INVALID_OR_EXPIRED_TOKEN";
    public const string AlreadyConfirmed = "ALREADY_CONFIRMED";
    public const string ResourceNotFound = "RESOURCE_NOT_FOUND";

    public static readonly Dictionary<string, (string Code, string Message)> Errors = new()
    {
        { nameof(EmailAlreadyInUse), ("EMAIL_ALREADY_IN_USE", "Email already in use") },
        { nameof(InvalidCredentials), ("INVALID_CREDENTIALS", "Invalid credentials") },
        { nameof(UserNotFound), ("USER_NOT_FOUND", "User not found") },
        { nameof(InvalidOrExpiredToken), ("INVALID_OR_EXPIRED_TOKEN", "Invalid or expired token") },
        { nameof(AlreadyConfirmed), ("ALREADY_CONFIRMED", "User is already confirmed") },
        { nameof(ResourceNotFound), ("RESOURCE_NOT_FOUND", "Resource not found") }
    };
}