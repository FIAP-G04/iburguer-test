using IBurguer.BDD.Model.Orders;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace IBurguer.BDD.Infrastructure.Orders
{
    public class OrdersService : IOrdersService
    {
        private readonly HttpClient _httpClient;
        private readonly OrdersServiceConfiguration _config;

        private readonly string _basePath = "/iburguer-admin/api/orders";

        public OrdersService(HttpClient httpClient, IOptions<OrdersServiceConfiguration> options)
        {
            _httpClient = httpClient;
            _config = options.Value;

            _httpClient.BaseAddress = new Uri(_config.BaseUrl);
        }

        public async Task Authenticate()
        {
            if (!_config.NeedsAuthentication)
                return;
        }

        public async Task<GeneratedOrderResponse> GenerateOrder(GenerateOrderRequest request)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_basePath}", content);

            response.EnsureSuccessStatusCode();

            var stringContent = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<GeneratedOrderResponse>(stringContent);

            return result;
        }

        public async Task<PaginatedList<OrderSummaryResponse>> GetOrders()
        {
            var response = await _httpClient.GetAsync($"{_basePath}?page=1&limit=10");
            response.EnsureSuccessStatusCode();

            var stringContent = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<PaginatedList<OrderSummaryResponse>>(stringContent);

            return result;
        }

        public async Task StartOrder(Guid orderId)
        {
            var response = await _httpClient.PatchAsync($"{_basePath}/{orderId}/started", new StringContent(""));

            response.EnsureSuccessStatusCode();
        }
    }
}
