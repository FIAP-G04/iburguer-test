using IBurguer.BDD.Infrastructure.ShoppingCart;

namespace IBurguer.BDD.StepDefinitions.ShoppingCart
{
    [Binding]
    public class ShoppingCartSteps
    {
        private readonly IShoppingCartService _shoppingCartService;
        private Guid _customerId;
        private Guid _shoppingCartId;

        public ShoppingCartSteps(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [Given(@"That i have a customer")]
        public void GivenThatIHaveACustomer()
        {
            _customerId = Guid.NewGuid();
        }

        [When(@"i create a shopping cart")]
        public async Task WhenICreateAShoppingCart()
        {
            var cart = await _shoppingCartService.Create(_customerId);
            _shoppingCartId = cart.ShoppingCartId;
        }

        [Then(@"the shopping cart must be created")]
        public async Task ThenTheShoppingCartMustBeCreated()
        {
            var cart = await _shoppingCartService.Get(_shoppingCartId);
            
            cart.Should().NotBeNull();
            cart.CustomerId.Should().Be(_customerId);
        }

    }
}
