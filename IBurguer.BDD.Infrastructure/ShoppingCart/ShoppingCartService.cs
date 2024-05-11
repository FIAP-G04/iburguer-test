using IBurguer.BDD.Model.ShoppingCart;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace IBurguer.BDD.Infrastructure.ShoppingCart
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpClient _httpClient;
        private readonly ShoppingCartConfiguration _configuration;

        private readonly string _path = "/api/carts";

        public ShoppingCartService(HttpClient client, IOptions<ShoppingCartConfiguration> options)
        {
            _httpClient = client;
            _configuration = options.Value;
            
            _httpClient.BaseAddress = new Uri(_configuration.BaseUrl);
        }

        public async Task Authenticate()
        {
            if (_configuration.NeedsAuthentication)
            {

            }
        }

        public async Task<CreateShoppingCartResult> Create(Guid customerId)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new { customerId = customerId}), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_path}", content);
            response.EnsureSuccessStatusCode();

            var strContent = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CreateShoppingCartResult>(strContent);

            return result;
        }

        public async Task<GetShoppingCartResult> Get(Guid shoppingCartId)
        {
            var response = await _httpClient.GetAsync($"{_path}/{shoppingCartId}");
            response.EnsureSuccessStatusCode();

            var strContent = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<GetShoppingCartResult>(strContent);
            return result;
        }
    }
}
