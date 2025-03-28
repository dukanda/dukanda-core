namespace DukandaCore.Application.Common.Enums;

public enum BookingStatusEnum
{
    Pending = 1,        // Initial state when booking is created
    Reserved = 2,       // Slots are temporarily reserved
    PaymentPending = 3, // Waiting for payment confirmation
    Confirmed = 4,      // Payment confirmed and booking is active
    Completed = 5,      // Tour has been completed
    Cancelled = 6,      // Booking was cancelled by user
    Expired = 7,        // Reserved booking expired (payment not received in time)
    Refunded = 8,       // Booking was refunded
    Failed = 9          // Payment or other process failed
}