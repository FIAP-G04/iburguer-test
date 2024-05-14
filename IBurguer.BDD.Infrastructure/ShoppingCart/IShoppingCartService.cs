using IBurguer.BDD.Model.ShoppingCart;

namespace IBurguer.BDD.Infrastructure.ShoppingCart
{
    public interface IShoppingCartService
    {
        Task Authenticate();

        Task<GetShoppingCartResult> Get(Guid shoppingCartId);

        Task<CreateShoppingCartResult> Create(Guid customerId);
    }
}
