namespace DukandaCore.Application.Common.Constants;


public static class ErrorCodes
{
    public const string EmailAlreadyInUse = "EMAIL_ALREADY_IN_USE";
    public const string InvalidCredentials = "INVALID_CREDENTIALS";
    public const string UserNotFound = "USER_NOT_FOUND";
    public const string InvalidOrExpiredToken = "INVALID_OR_EXPIRED_TOKEN";
    public const string AlreadyConfirmed = "ALREADY_CONFIRMED";
    public const string ResourceNotFound = "RESOURCE_NOT_FOUND";

    public const string PackageNotFound = "PACKAGE_NOT_FOUND";
    public const string InsufficientSlots = "INSUFFICIENT_SLOTS";
    public const string BookingNotFound = "BOOKING_NOT_FOUND";
    public const string InvalidPayment = "INVALID_PAYMENT";
    public const string PaymentFailed = "PAYMENT_FAILED";
    public const string BookingExpired = "BOOKING_EXPIRED";
    public const string TourNotAvailable = "TOUR_NOT_AVAILABLE";
    public const string InvalidBookingStatus = "INVALID_BOOKING_STATUS";
    public const string DuplicateBooking = "DUPLICATE_BOOKING";
    public const string InvalidQuantity = "INVALID_QUANTITY";

    public static readonly Dictionary<string, (string Code, string Message)> Errors = new()
    {
        { nameof(EmailAlreadyInUse), ("EMAIL_ALREADY_IN_USE", "Email already in use") },
        { nameof(InvalidCredentials), ("INVALID_CREDENTIALS", "Invalid credentials") },
        { nameof(UserNotFound), ("USER_NOT_FOUND", "User not found") },
        { nameof(InvalidOrExpiredToken), ("INVALID_OR_EXPIRED_TOKEN", "Invalid or expired token") },
        { nameof(AlreadyConfirmed), ("ALREADY_CONFIRMED", "User is already confirmed") },
        { nameof(ResourceNotFound), ("RESOURCE_NOT_FOUND", "Resource not found") },
        { nameof(PackageNotFound), ("PACKAGE_NOT_FOUND", "The requested package was not found") },
        { nameof(InsufficientSlots), ("INSUFFICIENT_SLOTS", "Not enough slots available for the requested quantity") },
        { nameof(BookingNotFound), ("BOOKING_NOT_FOUND", "The requested booking was not found") },
        { nameof(InvalidPayment), ("INVALID_PAYMENT", "The payment information is invalid or does not match") },
        { nameof(PaymentFailed), ("PAYMENT_FAILED", "The payment process failed") },
        { nameof(BookingExpired), ("BOOKING_EXPIRED", "The booking has expired") },
        { nameof(TourNotAvailable), ("TOUR_NOT_AVAILABLE", "The tour is not available for booking") },
        { nameof(InvalidBookingStatus), ("INVALID_BOOKING_STATUS", "Invalid booking status for the requested operation") },
        { nameof(DuplicateBooking), ("DUPLICATE_BOOKING", "A booking already exists for this user and tour") },
        { nameof(InvalidQuantity), ("INVALID_QUANTITY", "The requested quantity is invalid") }
    };
}