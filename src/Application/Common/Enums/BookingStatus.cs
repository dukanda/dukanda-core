namespace DukandaCore.Application.Common.Enums;

public enum BookingStatus
{
    Pending = 0,        // Initial state when booking is created
    Reserved = 1,       // Slots are temporarily reserved
    PaymentPending = 2, // Waiting for payment confirmation
    Confirmed = 3,      // Payment confirmed and booking is active
    Completed = 4,      // Tour has been completed
    Cancelled = 5,      // Booking was cancelled by user
    Expired = 6,        // Reserved booking expired (payment not received in time)
    Refunded = 7,       // Booking was refunded
    Failed = 8         // Payment or other process failed
} 