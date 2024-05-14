using IBurguer.BDD.Model.Orders;

namespace IBurguer.BDD.Infrastructure.Orders
{
    public interface IOrdersService
    {
        Task Authenticate();
        Task<GeneratedOrderResponse> GenerateOrder(GenerateOrderRequest request);

        Task<PaginatedList<OrderSummaryResponse>> GetOrders();
    }
}
