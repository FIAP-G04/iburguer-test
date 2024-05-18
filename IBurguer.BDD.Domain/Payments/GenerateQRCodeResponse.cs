namespace IBurguer.BDD.Model.Payments
{
    public class GenerateQRCodeResponse
    {
        public Guid PaymentId { get; set; }
        public Guid OrderId { get; set; }
        public string PaymentStatus { get; set; }
        public string QrData { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
