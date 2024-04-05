using IBurguer.BDD.Model.Menu;
using System.Text.Json;

namespace IBurguer.BDD.Infrastructure.Menu
{
    public class MenuService : IMenuService
    {
        private readonly HttpClient _client;

        private readonly string _path = "/items";

        public MenuService(HttpClient client)
        {
            _client = client;
        }

        public async Task Authenticate()
        {
            throw new NotImplementedException();
        }

        public async Task<AddMenuItemResponse> AddItem(AddMenuItemRequest request)
        {
            var content = new StringContent(JsonSerializer.Serialize(request));

            var response = await _client.PostAsync($"{_path}", content);
            response.EnsureSuccessStatusCode();

            var strContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<AddMenuItemResponse>(strContent);

            return result;
        }

        public async Task<IEnumerable<MenuItemResponse>> GetItems(string categoria)
        {
            var response = await _client.GetAsync($"{_path}?category={categoria}");
            response.EnsureSuccessStatusCode();

            var strContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<IEnumerable<MenuItemResponse>>(strContent);

            return result;
        }
    }
}
