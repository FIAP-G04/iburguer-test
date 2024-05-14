using IBurguer.BDD.Infrastructure.Orders;
using IBurguer.BDD.Infrastructure.Payments;
using IBurguer.BDD.Model.Orders;

namespace IBurguer.BDD.StepDefinitions.Payments
{
    [Binding]
    public class PaymentsSteps
    {
        private readonly IPaymentsService _paymentsService;
        private readonly IOrdersService _ordersService;

        private Guid _orderId;
        private Guid _paymentId;


        private decimal _price = 10;
        private ushort _quantity = 1;
        private decimal _amount => _price * _quantity;

        public PaymentsSteps(IPaymentsService paymentsService, IOrdersService ordersService)
        {
            _paymentsService = paymentsService;
            _ordersService = ordersService;
        }

        [Given(@"That I have an order waiting for payment")]
        public async Task GivenThatIHaveAnOrderWaitingForPayment()
        {
            var request = new GenerateOrderRequest()
            {
                OrderType = "EatIn",
                PaymentMethod = "QRCode",
                BuyerId = Guid.NewGuid(),
                Items = new OrderItemRequest[]
                    {
                        new()
                        {
                            ProductId = Guid.NewGuid(),
                            ProductName = "Test Product",
                            ProductType = "MainDish",
                            UnitPrice = _price,
                            Quantity = _quantity,
                        }
                    }
            };

            var result = await _ordersService.GenerateOrder(request);

            _orderId = result.OrderId;
        }

        [Given(@"That I generated a QR Code")]
        public async Task GivenThatIGeneratedAQRCode()
        {
            var result = await _paymentsService.GenerateQRCode(_orderId, _amount);

            _paymentId = result.PaymentId;
        }

        [When(@"The payment is confirmed")]
        public async Task WhenThePaymentIsConfirmed()
        {
            await _paymentsService.ConfirmPayment(_paymentId);
        }

        [Then(@"The order should be at Confirmed status")]
        public async Task ThenTheOrderShouldBeAtConfirmedStatus()
        {
            var orders = await _ordersService.GetOrders();

            var order = orders.Items.FirstOrDefault(x => x.OrderId == _orderId);

            order.Should().NotBeNull();

            order.OrderStatus.Should().Be("Confirmed");
        }

    }
}
