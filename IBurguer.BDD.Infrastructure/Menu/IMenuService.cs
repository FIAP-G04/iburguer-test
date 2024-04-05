using IBurguer.BDD.Model.Menu;

namespace IBurguer.BDD.Infrastructure.Menu
{
    public interface IMenuService
    {
        public Task Authenticate();

        public Task<AddMenuItemResponse> AddItem(AddMenuItemRequest request);

        public Task<IEnumerable<MenuItemResponse>> GetItems(string categoria);
    }
}
