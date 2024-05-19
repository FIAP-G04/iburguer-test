using IBurguer.BDD.Infrastructure.Orders;
using IBurguer.BDD.Infrastructure.Payments;
using IBurguer.BDD.Model.Orders;

namespace IBurguer.BDD.StepDefinitions.Ordering
{
    [Binding]
    public class OrderingSteps
    {
        private readonly IPaymentsService _paymentsService;
        private readonly IOrdersService _ordersService;

        private Guid _orderId;

        private decimal _price = 10;
        private ushort _quantity = 1;
        private decimal _amount => _price * _quantity;

        public OrderingSteps(IPaymentsService paymentsService, IOrdersService ordersService)
        {
            _paymentsService = paymentsService;
            _ordersService = ordersService;
        }

        [Given(@"That I have a paid order")]
        public async Task GivenThatIHaveAPaidOrder()
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

            var paymentResult = await _paymentsService.GenerateQRCode(_orderId, _amount);
            var paymentId = paymentResult.PaymentId;
            await _paymentsService.ConfirmPayment(paymentId);
        }

        [When(@"The the order is started")]
        public async Task WhenTheTheOrderIsStarted()
        {
            await _ordersService.StartOrder(_orderId);
        }

        [Then(@"The order should be at InProgress status")]
        public async Task ThenTheOrderShouldBeAtConfirmedStatus()
        {
            var orders = await _ordersService.GetOrders();

            var order = orders.Items.FirstOrDefault(x => x.OrderId == _orderId);

            order.Should().NotBeNull();

            order.OrderStatus.Should().Be("InProgress");
        }
    }
}
