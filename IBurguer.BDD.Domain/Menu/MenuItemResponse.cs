namespace IBurguer.BDD.Model.Menu
{
    public class MenuItemResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public ushort PreparationTime { get; set; }
        public string[] ImagesUrl { get; set; }
    }
}
