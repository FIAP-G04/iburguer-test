using IBurguer.BDD.Model.Payments;

namespace IBurguer.BDD.Infrastructure.Payments
{
    public interface IPaymentsService
    {
        Task<GenerateQRCodeResponse> GenerateQRCode(Guid orderId, decimal Amount);

        Task ConfirmPayment(Guid paymentId);
    }
}
