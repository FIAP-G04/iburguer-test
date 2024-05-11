namespace IBurguer.BDD.Model.ShoppingCart
{
    public class GetShoppingCartResult
    {
        public Guid ShoppingCartId { get; set; }
        public Guid CustomerId { get; set; }
        public bool Closed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Total { get; set; }
        public IList<GetShoppingCartItemResult> Items { get; set; }
    }

    public class GetShoppingCartItemResult
    {
        public Guid CartItemId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int Subtotal { get; set; }
    }
}
