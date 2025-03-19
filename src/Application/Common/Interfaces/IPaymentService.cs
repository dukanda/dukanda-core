public interface IPaymentService
{
    Task<bool> CreatePaymentIntentAsync(decimal amount);
    Task<bool> ConfirmPaymentIntentAsync(string paymentIntentId);
} 