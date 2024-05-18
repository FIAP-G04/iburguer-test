namespace IBurguer.BDD.Model.Orders
{
    public class OrderSummaryResponse
    {
        public Guid OrderId { get; set; }

        public int OrderNumber { get; set; }

        public string OrderType { get; set; }

        public string OrderStatus { get; set; }

        public string PaymentMethod { get; set; }

        public Guid? BuyerId { get; set; }

        public IList<OrderItemSummaryResponse> Items { get; set; } = new List<OrderItemSummaryResponse>();

        public decimal Total { get; set; }
    }

    public record OrderItemSummaryResponse()
    {
        public Guid OrderItemId { get; set; }

        public ushort Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Subtotal { get; set; }

        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductType { get; set; }
    }
}
