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

        private readonly string _path = "/iburguer-totem/api/carts";
        private readonly string _signInPath = "/iburguer-totem/signin";
        private readonly string _signUpPath = "/iburguer-totem/signup";

        private string _token = string.Empty;

        public ShoppingCartService(HttpClient client, IOptions<ShoppingCartConfiguration> options)
        {
            _httpClient = client;
            _configuration = options.Value;
            
            _httpClient.BaseAddress = new Uri(_configuration.BaseUrl);
        }

        public async Task Authenticate()
        {
            if (!_configuration.NeedsAuthentication)
                return;

            var cpf = "25603509040";

            var content = new StringContent(JsonConvert.SerializeObject(
                new
                {
                    cpf = cpf,
                    firstName = "Test Name",
                    lastName = "Test Last Name",
                    email = "bdd.test@iburguer.com"
                }), Encoding.UTF8, "application/json");

            await _httpClient.PostAsync($"{_signUpPath}", content);

            content = new StringContent(cpf, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_signInPath}", content);
            response.EnsureSuccessStatusCode();

            var strContent = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ShoppingCartAuthResult>(strContent);

            _token = result.AccessToken;
        }

        public async Task<CreateShoppingCartResult> Create(Guid customerId)
        {
            if(!string.IsNullOrEmpty(_token))
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
            }

            var content = new StringContent(JsonConvert.SerializeObject(new { customerId = customerId}), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_path}", content);
            response.EnsureSuccessStatusCode();

            var strContent = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CreateShoppingCartResult>(strContent);

            return result;
        }

        public async Task<GetShoppingCartResult> Get(Guid shoppingCartId)
        {
            if (!string.IsNullOrEmpty(_token))
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
            }

            var response = await _httpClient.GetAsync($"{_path}/{shoppingCartId}");
            response.EnsureSuccessStatusCode();

            var strContent = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<GetShoppingCartResult>(strContent);
            return result;
        }
    }
}
