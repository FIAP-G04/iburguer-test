using IBurguer.BDD.Model.Menu;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace IBurguer.BDD.Infrastructure.Menu
{
    public class MenuService : IMenuService
    {
        private readonly HttpClient _client;
        private readonly MenuServiceConfiguration _config;

        private readonly string _path = "/items";

        public MenuService(HttpClient client, IOptions<MenuServiceConfiguration> options)
        {
            _client = client;
            _config = options.Value;

            _client.BaseAddress = new Uri(_config.BaseUrl);
        }

        public async Task Authenticate()
        {
            if(_config.NeedsAuthentication)
            {

            }
        }

        public async Task<AddMenuItemResponse> AddItem(AddMenuItemRequest request)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{_path}", content);
            response.EnsureSuccessStatusCode();

            var strContent = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<AddMenuItemResponse>(strContent);

            return result;
        }

        public async Task<IEnumerable<MenuItemResponse>> GetItems(string categoria)
        {
            var response = await _client.GetAsync($"{_path}?category={categoria}");
            response.EnsureSuccessStatusCode();

            var strContent = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<MenuItemResponse>>(strContent);

            return result;
        }
    }
}
