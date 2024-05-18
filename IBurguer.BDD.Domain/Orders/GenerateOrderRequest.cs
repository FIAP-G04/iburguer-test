namespace IBurguer.BDD.Model.Orders
{
    public class GenerateOrderRequest
    {
        public string OrderType { get; set; } = string.Empty;
        public string PaymentMethod { get; init; } = string.Empty;
        public Guid? BuyerId { get; init; }
        public IList<OrderItemRequest> Items { get; set; } = new List<OrderItemRequest>();
    }

    public class OrderItemRequest
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductType { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public ushort Quantity { get; set; }
    }
}
